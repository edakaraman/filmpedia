function searchMovies(searchTerm) {
    const url = searchURL + "&query=" + searchTerm;

    fetch(url)
        .then((res) => res.json())
        .then((data) => {
            if (data.results.length > 0) {
                showMovies(data.results);
            } else {
                main.innerHTML = "<p>Arama sonucunda film bulunamad�.</p>";
            }
        })
        .catch((error) => {
            console.log("Hata: ", error);
            main.innerHTML = "<p>Arama s�ras�nda bir hata olu�tu.</p>";
        });
}
function showMovies(data) {
    main.innerHTML = "";
    data.forEach((movie) => {
        const { title, poster_path, vote_average, overview, id } = movie;
        const movieEl = document.createElement("div");
        movieEl.classList.add("movie");
        movieEl.innerHTML = `
          <img src="${BASE_IMG_URL + poster_path}" alt="${title}">
          <div class="movie-info">
            <h3>${title}</h3>
            <span class="${getColor(vote_average)}">${vote_average.toFixed(2)}</span>
          </div>
          <div class="overview">
            <h3>Overview</h3>
            ${overview.substring(0, 180)}...
          </div>
        `;
        main.appendChild(movieEl);

        document.getElementById(id).addEventListener("click", () => {
            search.value = title; // T�klanan filmin ba�l���n� arama kutusuna ata
            searchMovies(title); // Ba�l��a g�re arama yap
        });
    });
}
