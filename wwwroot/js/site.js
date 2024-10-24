<script>
    function addToWishlist(productId) {
        $.ajax({
            url: '/Access/AddToWishlist',
            type: 'POST',
            data: { productId: productId },
            success: function (response) {
                if (response.success) {
                    alert("Sản phẩm đã được thêm vào danh sách yêu thích.");
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Đã có lỗi xảy ra. Vui lòng thử lại.");
            }
        })
    };
    function showWishlist() {
    const isLoggedIn = '@User.Identity.IsAuthenticated'; // Kiểm tra đăng nhập

    if (!isLoggedIn) {
        alert('Bạn cần đăng nhập để xem danh sách yêu thích.');
    return;
    }

    // Nếu đã đăng nhập, lấy danh sách sản phẩm yêu thích từ server
    fetch('/Product/GetWishlistItems')
        .then(response => response.json())
        .then(data => {
            const wishlistList = document.getElementById('wishlistItems');
    wishlistList.innerHTML = ''; // Xóa danh sách cũ

            data.forEach(item => {
                const li = document.createElement('li');
    li.innerHTML = `<img src="${item.ImageUrl}" alt="${item.Name}" style="height: 100px;" /> ${item.Name} - ${item.Price}`;
                wishlistList.appendChild(li);
            });

            // Hiển thị modal
            $('#wishlistModal').modal('show');
        });
}      
</script>

