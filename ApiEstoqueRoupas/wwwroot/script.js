const API_URL = 'http://localhost:5123/api/products';

// Carrega os produtos quando a página é carregada
document.addEventListener('DOMContentLoaded', () => {
    loadAllProducts();
    updateStats();
});

// ===== NAVEGAÇÃO ENTRE SEÇÕES =====
function showSection(sectionName) {
    // Remove active de todos os botões e seções
    document.querySelectorAll('.menu-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelectorAll('.content-section').forEach(section => section.classList.remove('active'));
    
    // Ativa a seção selecionada
    document.getElementById(`section-${sectionName}`).classList.add('active');
    event.target.closest('.menu-btn').classList.add('active');
    
    // Se for a seção "todos", carrega os produtos
    if (sectionName === 'todos') {
        loadAllProducts();
    }
}

// ===== CADASTRO DE PRODUTO =====
async function addProduct() {
    const id = parseInt(document.getElementById('productId').value);
    const name = document.getElementById('productName').value.trim();
    const category = document.getElementById('productCategory').value;
    const quantity = parseInt(document.getElementById('productQuantity').value);
    const threshold = parseInt(document.getElementById('productThreshold').value);
    
    // Validação
    if (!id || !name || !category || isNaN(quantity) || isNaN(threshold)) {
        showMessage('cadastro-message', 'Por favor, preencha todos os campos obrigatórios!', 'error');
        return;
    }
    
    const product = {
        id: id,
        name: name,
        category: category,
        quantity: quantity,
        reorderThreshold: threshold
    };
    
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(product)
        });
        
        if (!response.ok) {
            const error = await response.text();
            throw new Error(error || 'Erro ao cadastrar produto');
        }
        
        showMessage('cadastro-message', `✓ Produto "${name}" cadastrado com sucesso!`, 'success');
        clearForm();
        updateStats();
        
    } catch (error) {
        console.error('Erro:', error);
        showMessage('cadastro-message', '✕ ' + error.message, 'error');
    }
}

// Limpa o formulário
function clearForm() {
    document.getElementById('productId').value = '';
    document.getElementById('productName').value = '';
    document.getElementById('productCategory').value = '';
    document.getElementById('productQuantity').value = '';
    document.getElementById('productThreshold').value = '5';
}

// ===== BUSCAR PRODUTOS =====
function switchSearchTab(tab) {
    // Remove active dos botões e painéis
    document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelectorAll('.search-panel').forEach(panel => panel.classList.remove('active'));
    
    // Ativa o tab selecionado
    event.target.classList.add('active');
    document.getElementById(`search-${tab}`).classList.add('active');
    
    // Limpa resultados
    document.getElementById('search-results').innerHTML = '<p class="empty-message">Nenhuma busca realizada ainda.</p>';
}

// Busca por ID
async function searchById() {
    const id = document.getElementById('searchIdInput').value;
    
    if (!id) {
        alert('Digite um ID para buscar!');
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/${id}`);
        
        if (!response.ok) {
            throw new Error('Produto não encontrado');
        }
        
        const product = await response.json();
        displaySearchResults([product]);
        
    } catch (error) {
        document.getElementById('search-results').innerHTML = 
            `<div class="error-message" style="display: block;">✕ ${error.message}</div>`;
    }
}

// Busca por Categoria
async function searchByCategory() {
    const category = document.getElementById('searchCategoryInput').value;
    
    if (!category) {
        alert('Selecione uma categoria para buscar!');
        return;
    }
    
    try {
        const response = await fetch(API_URL);
        
        if (!response.ok) {
            throw new Error('Erro ao buscar produtos');
        }
        
        const allProducts = await response.json();
        const filteredProducts = allProducts.filter(p => p.category === category);
        
        if (filteredProducts.length === 0) {
            document.getElementById('search-results').innerHTML = 
                '<p class="empty-message">Nenhum produto encontrado nesta categoria.</p>';
        } else {
            displaySearchResults(filteredProducts);
        }
        
    } catch (error) {
        document.getElementById('search-results').innerHTML = 
            `<div class="error-message" style="display: block;">✕ ${error.message}</div>`;
    }
}

// Exibe resultados da busca
function displaySearchResults(products) {
    const resultsDiv = document.getElementById('search-results');
    
    resultsDiv.innerHTML = `
        <h3 class="results-title">Resultados (${products.length})</h3>
        <div class="results-grid">
            ${products.map(product => `
                <div class="product-card">
                    <div class="card-header">
                        <span class="card-id">#${product.id}</span>
                        ${product.quantity <= product.reorderThreshold ? 
                            '<span class="badge badge-warning">⚠️ Estoque Baixo</span>' : 
                            '<span class="badge badge-ok">✓ OK</span>'}
                    </div>
                    <h3 class="card-title">${product.name}</h3>
                    <div class="card-info">
                        <p><strong>Categoria:</strong> ${product.category}</p>
                        <p><strong>Quantidade:</strong> ${product.quantity}</p>
                        <p><strong>Reposição Mín.:</strong> ${product.reorderThreshold}</p>
                    </div>
                    <button class="btn-delete-card" onclick="deleteProduct(${product.id})">
                        🗑️ DELETAR
                    </button>
                </div>
            `).join('')}
        </div>
    `;
}

// ===== LISTAR TODOS OS PRODUTOS =====
async function loadAllProducts() {
    const loading = document.getElementById('loading');
    const errorMessage = document.getElementById('error-message');
    const productList = document.getElementById('productList');
    
    loading.style.display = 'flex';
    errorMessage.style.display = 'none';
    
    try {
        const response = await fetch(API_URL);
        
        if (!response.ok) {
            throw new Error('Erro ao carregar produtos');
        }
        
        const products = await response.json();
        
        productList.innerHTML = '';
        
        if (products.length === 0) {
            productList.innerHTML = '<tr><td colspan="7" class="empty-message">Nenhum produto cadastrado</td></tr>';
        } else {
            products.forEach(product => {
                const lowStock = product.quantity <= product.reorderThreshold;
                const statusClass = lowStock ? 'status-low' : 'status-ok';
                const statusText = lowStock ? '⚠️ Baixo' : '✓ OK';
                
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
                            🗑️ DELETAR
                        </button>
                    </td>
                `;
                productList.appendChild(row);
            });
        }
        
        updateStats();
        
    } catch (error) {
        console.error('Erro:', error);
        errorMessage.textContent = '✕ Erro ao carregar produtos. Tente novamente.';
        errorMessage.style.display = 'block';
    } finally {
        loading.style.display = 'none';
    }
}

// ===== DELETAR PRODUTO =====
async function deleteProduct(id, reload = false) {
    if (!confirm('Tem certeza que deseja deletar este produto?')) {
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE'
        });
        
        if (!response.ok) {
            throw new Error('Erro ao deletar produto');
        }
        
        alert('✓ Produto deletado com sucesso!');
        
        if (reload) {
            loadAllProducts();
        } else {
            // Remove do resultado da busca
            event.target.closest('.product-card').remove();
        }
        
        updateStats();
        
    } catch (error) {
        console.error('Erro:', error);
        alert('✕ Erro ao deletar produto. Tente novamente.');
    }
}

// ===== ESTATÍSTICAS =====
async function updateStats() {
    try {
        const response = await fetch(API_URL);
        
        if (!response.ok) return;
        
        const products = await response.json();
        
        document.getElementById('totalProducts').textContent = products.length;
        
        const lowStockCount = products.filter(p => p.quantity <= p.reorderThreshold).length;
        document.getElementById('lowStock').textContent = lowStockCount;
        
    } catch (error) {
        console.error('Erro ao atualizar estatísticas:', error);
    }
}

// ===== MENSAGENS =====
function showMessage(elementId, message, type) {
    const messageBox = document.getElementById(elementId);
    messageBox.className = `message-box ${type}`;
    messageBox.textContent = message;
    messageBox.style.display = 'block';
    
    setTimeout(() => {
        messageBox.style.display = 'none';
    }, 5000);
}

// Enter para buscar
document.getElementById('searchIdInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') searchById();
});

document.getElementById('productThreshold').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') addProduct();
});
