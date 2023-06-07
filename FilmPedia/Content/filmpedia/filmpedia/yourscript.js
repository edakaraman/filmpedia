<script>
    function deleteFilm(filmTitle) {
        const request = new XMLHttpRequest();
    const url = "/FavoriteList/DeleteFavFilm"; // Silme iþlemini gerçekleþtiren Controller ve Action'ýn URL'sini buraya ekleyin

    request.open("POST", url);
    request.setRequestHeader("Content-Type", "application/json");
    request.onload = function () {
            if (request.status === 200) {
        // Ýstek baþarýyla tamamlandý
        console.log("Film baþarýyla silindi.");
            } else {
        // Ýstek sýrasýnda bir hata oluþtu
        console.log("Hata: " + request.statusText);
            }
        };
    request.onerror = function () {
        // Ýstek gönderilirken bir hata oluþtu
        console.log("Hata: Ýstek gönderilirken bir sorun oluþtu.");
        };
    request.send(JSON.stringify(filmTitle));
    }
</script>
