<script>
    function deleteFilm(filmTitle) {
        const request = new XMLHttpRequest();
    const url = "/FavoriteList/DeleteFavFilm"; // Silme i�lemini ger�ekle�tiren Controller ve Action'�n URL'sini buraya ekleyin

    request.open("POST", url);
    request.setRequestHeader("Content-Type", "application/json");
    request.onload = function () {
            if (request.status === 200) {
        // �stek ba�ar�yla tamamland�
        console.log("Film ba�ar�yla silindi.");
            } else {
        // �stek s�ras�nda bir hata olu�tu
        console.log("Hata: " + request.statusText);
            }
        };
    request.onerror = function () {
        // �stek g�nderilirken bir hata olu�tu
        console.log("Hata: �stek g�nderilirken bir sorun olu�tu.");
        };
    request.send(JSON.stringify(filmTitle));
    }
</script>
