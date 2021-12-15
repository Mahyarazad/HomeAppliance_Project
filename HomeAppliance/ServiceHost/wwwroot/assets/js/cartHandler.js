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

$(".header-cart-icon").on('mouseover', function (e) {
    e.preventDefault;
    $("#mini-cart").addClass('active');
});

$(".header-cart-icon").on('mouseleave', function (e) {

    var offsetLeft = $(".header-cart-icon").offset().left;
    var offsetTop = $(".header-cart-icon").offset().top;
    var elementWidth = $(".header-cart-icon").outerWidth();

    if (e.clientX > offsetLeft + elementWidth || e.clientX < offsetLeft || e.clientY < offsetTop) {
        $("#mini-cart").removeClass('active');
    }
});

$("#mini-cart").on('mouseleave', function (e) {
    e.preventDefault;
    $("#mini-cart").removeClass('active');
});


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

function updateMiniCart() {
    var cartObject;
    const cart = $.cookie("cart-items");
    const wrapper = $('#cart-items-wrapper');
    wrapper.html('');
    if (cart !== undefined) {
        cartObject = JSON.parse(cart);
        $("span[id='header-counter']").text(Object.keys(cartObject).length);
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
        });
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

    if (e.clientX > offsetLeft + elementWidth || e.clientX < offsetLeft || e.clientY < offsetTop) {
        $(".settings-menu-wrapper").removeClass('active');
    }
});

$("#settings-menu-wrapper").on('mouseleave', function () {
    $(".settings-menu-wrapper").removeClass("active");
});