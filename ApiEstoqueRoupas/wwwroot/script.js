const API_URL = 'http://localhost:5123/api';

// Carrega dados ao iniciar
document.addEventListener('DOMContentLoaded', () => {
    loadAllProducts();
    updateStats();
    loadRestockAlerts();
    updateAlertsBadge();
    
    setInterval(() => {
        updateStats();
        updateAlertsBadge();
    }, 30000);
});

//  NAVEGAÇÃO 
function showSection(sectionName) {
    document.querySelectorAll('.menu-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelectorAll('.content-section').forEach(section => section.classList.remove('active'));
    
    document.getElementById(`section-${sectionName}`).classList.add('active');
    event.target.closest('.menu-btn').classList.add('active');
    
    if (sectionName === 'todos') loadAllProducts();
    if (sectionName === 'historico') loadMovements();
    if (sectionName === 'alertas') loadRestockAlerts();
}

//  CADASTRO 
async function addProduct() {
    const id = parseInt(document.getElementById('productId').value);
    const name = document.getElementById('productName').value.trim();
    const category = document.getElementById('productCategory').value;
    const quantity = parseInt(document.getElementById('productQuantity').value);
    const threshold = parseInt(document.getElementById('productThreshold').value);
    
    if (!id || !name || !category || isNaN(quantity) || isNaN(threshold)) {
        showMessage('cadastro-message', 'Preencha todos os campos!', 'error');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/products`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id, name, category, quantity, reorderThreshold: threshold })
        });
        
        if (!response.ok) throw new Error(await response.text());
        
        showMessage('cadastro-message', `✓ Produto "${name}" cadastrado!`, 'success');
        clearForm();
        updateStats();
    } catch (error) {
        showMessage('cadastro-message', '✕ ' + error.message, 'error');
    }
}

function clearForm() {
    ['productId', 'productName', 'productQuantity'].forEach(id => 
        document.getElementById(id).value = '');
    document.getElementById('productCategory').value = '';
    document.getElementById('productThreshold').value = '5';
}

//  MOVIMENTAÇÃO 
function switchMovementTab(type) {
    document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelectorAll('.movement-panel').forEach(panel => panel.classList.remove('active'));
    
    event.target.classList.add('active');
    document.getElementById(`movement-${type}`).classList.add('active');
}

async function registerEntry() {
    const productId = parseInt(document.getElementById('entryProductId').value);
    const quantity = parseInt(document.getElementById('entryQuantity').value);
    const reason = document.getElementById('entryReason').value;
    const user = document.getElementById('entryUser').value;
    
    if (!productId || !quantity || quantity <= 0) {
        showMessage('movement-message', 'Preencha ID e quantidade válidos!', 'error');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/stock/entry`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ productId, quantity, reason, user })
        });
        
        if (!response.ok) throw new Error(await response.text());
        
        const data = await response.json();
        showMessage('movement-message', `✓ ${data.message}`, 'success');
        
        document.getElementById('entryProductId').value = '';
        document.getElementById('entryQuantity').value = '';
        
        updateStats();
        loadAllProducts();
    } catch (error) {
        showMessage('movement-message', '✕ ' + error.message, 'error');
    }
}

async function registerExit() {
    const productId = parseInt(document.getElementById('exitProductId').value);
    const quantity = parseInt(document.getElementById('exitQuantity').value);
    const reason = document.getElementById('exitReason').value;
    const user = document.getElementById('exitUser').value;
    
    if (!productId || !quantity || quantity <= 0) {
        showMessage('movement-message', 'Preencha ID e quantidade válidos!', 'error');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/stock/exit`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ productId, quantity, reason, user })
        });
        
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Erro ao registrar saída');
        }
        
        const data = await response.json();
        const messageType = data.needsRestock ? 'warning' : 'success';
        showMessage('movement-message', data.message, messageType);
        
        document.getElementById('exitProductId').value = '';
        document.getElementById('exitQuantity').value = '';
        
        updateStats();
        loadAllProducts();
        
        if (data.needsRestock) {
            setTimeout(() => loadRestockAlerts(), 500);
        }
    } catch (error) {
        showMessage('movement-message', '✕ ' + error.message, 'error');
    }
}

//  BUSCA 
function switchSearchTab(type) {
    document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelectorAll('.search-panel').forEach(panel => panel.classList.remove('active'));
    
    event.target.classList.add('active');
    document.getElementById(`search-${type}`).classList.add('active');
    
    document.getElementById('searchResults').style.display = 'none';
}

async function searchById() {
    const id = parseInt(document.getElementById('searchIdInput').value);
    
    if (!id) {
        alert('Digite um ID válido!');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/products/${id}`);
        
        if (!response.ok) {
            throw new Error('Produto não encontrado');
        }
        
        const product = await response.json();
        displaySearchResults([product]);
        
    } catch (error) {
        alert('✕ ' + error.message);
        document.getElementById('searchResults').style.display = 'none';
    }
}

async function searchByCategory() {
    const category = document.getElementById('searchCategorySelect').value;
    
    if (!category) {
        alert('Selecione uma categoria!');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/products`);
        if (!response.ok) throw new Error('Erro ao buscar produtos');
        
        const allProducts = await response.json();
        const filtered = allProducts.filter(p => p.category === category);
        
        if (filtered.length === 0) {
            alert('Nenhum produto encontrado nesta categoria');
            document.getElementById('searchResults').style.display = 'none';
            return;
        }
        
        displaySearchResults(filtered);
        
    } catch (error) {
        alert('✕ ' + error.message);
    }
}

function displaySearchResults(products) {
    const resultsDiv = document.getElementById('searchResults');
    const gridDiv = document.getElementById('resultsGrid');
    
    resultsDiv.style.display = 'block';
    
    gridDiv.innerHTML = products.map(product => {
        const lowStock = product.quantity <= product.reorderThreshold;
        const outOfStock = product.quantity === 0;
        
        let badgeClass, badgeText;
        if (outOfStock) {
            badgeClass = 'badge-warning';
            badgeText = '🔴 ESGOTADO';
        } else if (lowStock) {
            badgeClass = 'badge-warning';
            badgeText = '⚠️ Baixo';
        } else {
            badgeClass = 'badge-ok';
            badgeText = '✓ OK';
        }
        
        return `
            <div class="product-card">
                <div class="card-header">
                    <span class="card-id">#${product.id}</span>
                    <span class="badge ${badgeClass}">${badgeText}</span>
                </div>
                <h3 class="card-title">${product.name}</h3>
                <div class="card-info">
                    <p><strong>Categoria:</strong> ${product.category}</p>
                    <p><strong>Quantidade:</strong> ${product.quantity}</p>
                    <p><strong>Estoque Mínimo:</strong> ${product.reorderThreshold}</p>
                </div>
                <button class="btn-delete-card" onclick="deleteProduct(${product.id}, false)">
                    🗑️ DELETAR
                </button>
            </div>
        `;
    }).join('');
}

// HISTÓRICO 
let currentFilter = 'all';

async function loadMovements(type = currentFilter) {
    currentFilter = type;
    const container = document.getElementById('movements-container');
    container.innerHTML = '<p class="empty-message">Carregando...</p>';
    
    try {
        const url = type === 'all' 
            ? `${API_URL}/stock/movements`
            : `${API_URL}/stock/movements?type=${type}`;
            
        const response = await fetch(url);
        if (!response.ok) throw new Error('Erro ao carregar histórico');
        
        const movements = await response.json();
        
        if (movements.length === 0) {
            container.innerHTML = '<p class="empty-message">Nenhuma movimentação encontrada</p>';
            return;
        }
        
        container.innerHTML = movements.map(m => `
            <div class="movement-card ${m.type.toLowerCase()}">
                <div class="movement-header">
                    <span class="movement-type ${m.type.toLowerCase()}">${m.type === 'ENTRADA' ? '📥' : '📤'} ${m.type}</span>
                    <span class="movement-date">${formatDate(m.date)}</span>
                </div>
                <h3>${m.productName}</h3>
                <div class="movement-details">
                    <p><strong>Quantidade:</strong> ${m.quantity}</p>
                    <p><strong>Estoque:</strong> ${m.stockBefore} → ${m.stockAfter}</p>
                    <p><strong>Motivo:</strong> ${m.reason}</p>
                    <p><strong>Usuário:</strong> ${m.user}</p>
                </div>
            </div>
        `).join('');
        
    } catch (error) {
        container.innerHTML = `<p class="error-message">✕ ${error.message}</p>`;
    }
}

function filterMovements(type) {
    document.querySelectorAll('.filter-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    loadMovements(type);
}

// ALERTAS 
async function loadRestockAlerts() {
    const container = document.getElementById('alerts-container');
    container.innerHTML = '<p class="empty-message">Carregando...</p>';
    
    try {
        const response = await fetch(`${API_URL}/stock/restock-alerts`);
        if (!response.ok) throw new Error('Erro ao carregar alertas');
        
        const data = await response.json();
        
        if (data.count === 0) {
            container.innerHTML = '<p class="empty-message">✓ Nenhum produto precisa de reposição!</p>';
            return;
        }
        
        container.innerHTML = data.alerts.map(alert => `
            <div class="alert-card ${alert.alertLevel.toLowerCase()}">
                <div class="alert-icon">${alert.alertLevel === 'CRITICAL' ? '🔴' : '⚠️'}</div>
                <div class="alert-content">
                    <h3>${alert.productName}</h3>
                    <span class="alert-category">${alert.category}</span>
                    <div class="alert-details">
                        <p><strong>Estoque Atual:</strong> ${alert.currentStock}</p>
                        <p><strong>Estoque Mínimo:</strong> ${alert.reorderThreshold}</p>
                        <p class="suggestion">💡 Sugestão: Comprar ${alert.suggestedOrderQuantity} unidades</p>
                    </div>
                    <button class="btn-restock" onclick="quickRestock(${alert.productId}, ${alert.suggestedOrderQuantity})">
                        📥 REABASTECER
                    </button>
                </div>
            </div>
        `).join('');
        
    } catch (error) {
        container.innerHTML = `<p class="error-message">✕ ${error.message}</p>`;
    }
}

async function quickRestock(productId, quantity) {
    if (!confirm(`Reabastecer com ${quantity} unidades?`)) return;
    
    try {
        const response = await fetch(`${API_URL}/stock/entry`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                productId,
                quantity,
                reason: 'Reabastecimento automático',
                user: 'Sistema'
            })
        });
        
        if (!response.ok) throw new Error('Erro ao reabastecer');
        
        alert('✓ Reabastecimento realizado com sucesso!');
        loadRestockAlerts();
        updateStats();
    } catch (error) {
        alert('✕ ' + error.message);
    }
}

async function updateAlertsBadge() {
    try {
        const response = await fetch(`${API_URL}/stock/restock-alerts`);
        const data = await response.json();
        
        const badge = document.getElementById('alertBadge');
        badge.textContent = data.count;
        badge.className = data.count > 0 ? 'alert-badge active' : 'alert-badge';
    } catch (error) {
        console.error('Erro ao atualizar badge:', error);
    }
}

//  ESTATÍSTICAS 
async function updateStats() {
    try {
        const response = await fetch(`${API_URL}/stock/report`);
        if (!response.ok) return;
        
        const stats = await response.json();
        
        document.getElementById('totalProducts').textContent = stats.totalProducts;
        document.getElementById('lowStock').textContent = stats.lowStockCount;
        document.getElementById('todayEntries').textContent = stats.todayEntries;
        document.getElementById('todayExits').textContent = stats.todayExits;
        
    } catch (error) {
        console.error('Erro ao atualizar estatísticas:', error);
    }
}

//  LISTAR PRODUTOS 
async function loadAllProducts() {
    const loading = document.getElementById('loading');
    const errorMessage = document.getElementById('error-message');
    const productList = document.getElementById('productList');
    
    if (loading) loading.style.display = 'flex';
    if (errorMessage) errorMessage.style.display = 'none';
    
    try {
        const response = await fetch(`${API_URL}/products`);
        if (!response.ok) throw new Error('Erro ao carregar produtos');
        
        const products = await response.json();
        
        if (productList) {
            productList.innerHTML = '';
            
            if (products.length === 0) {
                productList.innerHTML = '<tr><td colspan="7" class="empty-message">Nenhum produto cadastrado</td></tr>';
            } else {
                products.forEach(product => {
                    const lowStock = product.quantity <= product.reorderThreshold;
                    const outOfStock = product.quantity === 0;
                    
                    let statusClass, statusText;
                    if (outOfStock) {
                        statusClass = 'status-critical';
                        statusText = '🔴 ESGOTADO';
                    } else if (lowStock) {
                        statusClass = 'status-low';
                        statusText = '⚠️ Baixo';
                    } else {
                        statusClass = 'status-ok';
                        statusText = '✓ OK';
                    }
                    
                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td><strong>#${product.id}</strong></td>
                        <td>${product.name}</td>
                        <td><span class="category-badge">${product.category}</span></td>
                        <td><strong>${product.quantity}</strong></td>
                        <td>${product.reorderThreshold}</td>
                        <td><span class="status-badge ${statusClass}">${statusText}</span></td>
                        <td>
                            <button class="btn-delete" onclick="deleteProduct(${product.id}, true)">
                                🗑️
                            </button>
                        </td>
                    `;
                    productList.appendChild(row);
                });
            }
        }
        
        updateStats();
        
    } catch (error) {
        console.error('Erro:', error);
        if (errorMessage) {
            errorMessage.textContent = '✕ ' + error.message;
            errorMessage.style.display = 'block';
        }
    } finally {
        if (loading) loading.style.display = 'none';
    }
}

// DELETAR PRODUTO 
async function deleteProduct(id, reload = false) {
    if (!confirm('Deletar este produto?')) return;
    
    try {
        const response = await fetch(`${API_URL}/products/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Erro ao deletar');
        
        alert('✓ Produto deletado!');
        if (reload) {
            loadAllProducts();
        } else {
            document.getElementById('searchResults').style.display = 'none';
        }
        updateStats();
    } catch (error) {
        alert('✕ ' + error.message);
    }
}

// UTILITÁRIOS 
function showMessage(elementId, message, type) {
    const messageBox = document.getElementById(elementId);
    if (!messageBox) return;
    
    messageBox.className = `message-box ${type}`;
    messageBox.textContent = message;
    messageBox.style.display = 'block';
    
    setTimeout(() => messageBox.style.display = 'none', 5000);
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}