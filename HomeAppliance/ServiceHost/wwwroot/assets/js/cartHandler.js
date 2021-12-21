const notifyStyleClass = {
    showClass: {
        popup: 'ltr swal2-show',
        backdrop: 'swal2-backdrop-show',
        icon: 'swal2-icon-show'
    },
    hideClass: {
        popup: 'swal2-hide ltr',
        backdrop: 'swal2-backdrop-hide',
        icon: 'swal2-icon-hide'
    },
}



function successMessage() {
    Swal.fire({
        ...notifyStyleClass,
        position: 'top-start',
        icon: 'success',
        title: 'The item added to your cart.',
        showConfirmButton: false,
        timer: 1000
    })
}

function cartObjectMaker(id, name, price, picture, categoryPicture, productCount) {
    const objecttoReturn = {
        id: id,
        count: productCount,
        name: name,
        price: price,
        picture: picture,
        categoryPicture: categoryPicture
    }
    return objecttoReturn
}

function addToCart(id, name, price, picture, categoryPicture) {
    var productCount = parseInt($("#sale-value").val());
    const cartProduct = $.cookie("cart-items");
    var cartObject;

    if (cartProduct === undefined) {
        const product = cartObjectMaker(id, name, price, picture, categoryPicture, productCount);
        cartObject = [];
        cartObject.push(product);
        $.cookie("cart-items", JSON.stringify(cartObject), { expires: 1, path: '/', secure: false });
        updateMiniCart();
        successMessage();
        return;
    } else {
        cartObject = JSON.parse(cartProduct)
        const target = cartObject.find(x => x.id === id);
        if (target === undefined) {
            const product = cartObjectMaker(id, name, price, picture, categoryPicture, productCount);
            cartObject.push(product);
            $.cookie("cart-items", JSON.stringify(cartObject), { expires: 1, path: '/', secure: false });
            updateMiniCart();
            successMessage();
            return;
        }
        if (target.id === id) {
            cartObject.find(x => x.id === id).count = target.count + productCount;
            $.cookie("cart-items", JSON.stringify(cartObject), { expires: 1, path: '/', secure: false });
            updateMiniCart();
            successMessage();

            return;
        }

    }
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function updateMiniCart() {
    var cartObject;
    const cart = $.cookie("cart-items");
    const wrapper = $('#cart-items-wrapper');
    wrapper.html('');
    if (cart !== undefined) {
        cartObject = JSON.parse(cart);
        $("span[id='header-counter']").text(Object.keys(cartObject).length);
        $("span[id='mobile-cart']").text(Object.keys(cartObject).length);
        var totalAmount = 0;
        cartObject.forEach(item => {
            singleCartDom = `<div class="single-cart-item" id="single-cart">
                                        <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${item.id}')">
                                            <i class="ion-android-close"></i>
                                        </a>
                                        <div class="image">
                                            <a href="single-product.html">
                                                <img src="/Images/${item.categoryPicture}/${item.picture.slice(0, item.picture.length - 4)}/${item.picture}"
                                                     class="img-fluid" alt="">
                                            </a>
                                        </div>
                                        <div class="content" id="item-control">
                                            <p class="product-title">
                                                <a href="single-product.html">${item.name}</a>
                                            </p>
                                            <p class="count" id="${item.id}">$${item.price} &#215 <span>${item.count}</span>  </p>
                                        </div>
                                    </div>`;
            wrapper.append(singleCartDom);
            totalAmount += item.count * parseInt(item.price);
        });
        totalAmount = totalAmount.toFixed(2);
        var tax = (totalAmount * 0.2).toFixed(2);
        var totalAmountWithTax = (totalAmount * 1.2).toFixed(2)
        $("td[id='total-amount']").text(`${numberWithCommas(totalAmount)} $`);
        $("td[id='total-amount-plus-tax']").text(`${numberWithCommas(totalAmountWithTax)} $`);
        $("td[id='tax']").text(`${numberWithCommas(tax)} $`);

        $("span[id='total-amount']").text(`${numberWithCommas(totalAmount)} $`);
        $("span[id='total-amount-with-tax']").text(`${numberWithCommas(totalAmountWithTax)} $`);
        $("span[id='tax']").text(`${numberWithCommas(tax)} $`);
    }
}

function removeFromCart(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-warning',
            cancelButton: 'btn btn-default'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        ...notifyStyleClass,
        title: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, remove it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {

        if (result.isConfirmed) {
            const cart = $.cookie("cart-items");
            const cartObject = JSON.parse(cart);
            const target = cartObject.find(x => x.id === id);
            cartObject.splice(target, 1);
            $.cookie("cart-items", JSON.stringify(cartObject), { expires: 1, path: '/', secure: false });
            updateMiniCart();
            Swal.fire({
                ...notifyStyleClass,
                position: 'center',
                icon: 'success',
                title: 'The item has been removed.',
                showConfirmButton: false,
                timer: 1000
            })
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            //            swalWithBootstrapButtons.fire(
            //                'Cancelled',
            //                'Your imaginary file is safe :)',
            //                'error'
            //            )
        }
    })
}



$("#header-settings-trigger").on('mouseenter', function () {
    $(".settings-menu-wrapper").addClass("active");
});

$("#header-settings-trigger").on('mouseleave', function (e) {

    var offsetLeft = $("#header-settings-trigger").offset().left;
    var offsetTop = $("#header-settings-trigger").offset().top;
    var elementWidth = $("#header-settings-trigger").outerWidth();
    if (e.clientX > offsetLeft + elementWidth || e.clientX < offsetLeft || e.pageY < offsetTop) {
        $(".settings-menu-wrapper").removeClass('active');
    }
});

$("#settings-menu-wrapper").on('mouseleave', function () {
    console.log('=======');
    $(".settings-menu-wrapper").removeClass("active");
});


$(".header-cart-icon").on('mouseover', function (e) {
    e.preventDefault;
    $("#mini-cart").addClass('active');
});

$(".header-cart-icon").on('mouseleave', function (e) {

    var offsetLeft = $(".header-cart-icon").offset().left;
    var offsetTop = $(".header-cart-icon").offset().top;
    var elementWidth = $(".header-cart-icon").outerWidth();

    if (e.clientX > offsetLeft + elementWidth || e.clientX < offsetLeft || e.pageY < offsetTop) {
        $("#mini-cart").removeClass('active');
    }
});

$("#mini-cart").on('mouseleave', function (e) {
    e.preventDefault;
    $("#mini-cart").removeClass('active');
});


$(document).ready(() => {
    const cookie = $.cookie("cart-items");
    const cookieObject = JSON.parse(cookie);
    cookieObject.forEach(item => {
        var settings = {
            "url": "https://localhost:5000/api/inventory",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "ProductId": item.id,
                "Count": item.count
            }),
        };

        $.ajax(settings).done(function (response) {
            var wrapper = $('#inventory-warning');
            var target = $(`div[id='${item.id}']`);
            console.log(response);
            if (response.isInStock == false) {
                if (target.length == 0) {
                    wrapper.append(`<div class="alert alert-warning ltr d-flex" id="${item.id}">
                            <i class="fa fa-warning pr-1"></i>
                            <p class="pull-left"><b>${response.productName
                        }</b> stock is less than your ordered quantity, please modify your cart.</p>
                        </div>`);
                }

            } else {
                target.remove();
            }
        });
    });
})


function updateCart(id, count) {
    const cookie = $.cookie("cart-items");
    const cookieObject = JSON.parse(cookie);
    const target = cookieObject.findIndex(x => x.id === id);
    cookieObject[target].count = count;
    const priceToShow = (count * parseInt(cookieObject[target].price)).toFixed(2);
    $("span[id='item-total-amount']").text(`${priceToShow} $`);
    $.cookie("cart-items", JSON.stringify(cookieObject), { expires: 1, path: '/', secure: false });
    updateMiniCart();

    var settings = {
        "url": "https://localhost:5000/api/inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "ProductId": id,
            "Count": count
        }),
    };

    $.ajax(settings).done(function (response) {
        var wrapper = $('#inventory-warning');
        var target = $(`div[id='${id}']`);
        console.log(response);
        if (response.isInStock == false) {
            if (target.length == 0) {
                wrapper.append(`<div class="alert alert-warning ltr d-flex" id="${id}">
                            <i class="fa fa-warning pr-1"></i>
                            <p class="pull-left"><b>${response.productName
                    }</b> stock is less than your ordered quantity, please modify your cart.</p>
                        </div>`);
            }

        } else {
            target.remove();
        }
    });
}