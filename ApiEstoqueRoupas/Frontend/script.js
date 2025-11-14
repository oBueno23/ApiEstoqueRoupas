function addProduct() {
    const input = document.getElementById('productInput');
    const productName = input.value.trim();
    
    if (productName === '') {
        alert('Por favor, digite o nome do produto');
        return;
    }

    const productList = document.getElementById('productList');
    
    const productItem = document.createElement('div');
    productItem.className = 'product-item';
    
    productItem.innerHTML = `
        <span class="product-name">${productName}</span>
        <button class="btn-deletar" onclick="deleteProduct(this)">DELETAR</button>
    `;
    
    productList.appendChild(productItem);
    input.value = '';
    input.focus();
}

function deleteProduct(button) {
    const productItem = button.parentElement;
    productItem.remove();
}

document.getElementById('productInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') {
        addProduct();
    }
});