if (document.readyState == 'loading') {
    document.addEventListener('DOMcontentloaded', ready)
} else {
    ready()
}

function ready() {

    var quantityInputs = document.getElementById('cart-quantity-input')
    for (var i = 0; i < quantityInputs.length; i++) {
        var input = quantityInputs[i]
        input.addEventListener('change', quantitychanged)
    }
}

function removeCartItem(event) {
    var buttonClicked = event.target
    buttonClicked.parentElement.parentElement.remove()
    updateCartTotal()
}

function quantitychanged(event) {
    var input = event.target
    if (isNaN(input.value) || input.value <= 0) {
        input.value=1
    }
}

function updateCartTotal() {
    var cartItemContainner = document.getElementsByClassName('cart-items')[0]
    var cartRows = cartItemContainner.getElementsByClassName('cart-row')
    var subtotal = 0
    var total = 0
    for (var i = 0; i < cartRows.length; i++) {
        var cartRow = cartRows[i]
        var priceElement = cartRow.getElementsByClassName('cart-price')[0]
        var quantityElement = cartRow.getElementsByClassName('cart-quantity-input')[0]
        var price = parseFloat(priceElement.innerText)
        var quantity = quantityElement.value
        subtotal = price * quantity
        total = total + (price * quantity)
    }
    document.getElementsByClassName('cart-subs-price')[0].innerText = subtotal + ' VNĐ'
    document.getElementsByClassName('cart-total-price')[0].innerText = total + ' VNĐ'
}