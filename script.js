const API_KEY = "api_key=1cf50e6248dc270629e802686245c2c8";
const BASE_URL = "https://api.themoviedb.org/3";
const API_URL = BASE_URL + "/discover/movie?sort_by=popularity.desc&" + API_KEY;
const IMG_URL = "https://image.tmdb.org/t/p/w500";
const searchURL = BASE_URL + "/search/movie?" + API_KEY;

/* home.html dosyasındaki bazı verileri burda kullanmak için 
idsi ile elemanları değişkenlere atama */
const main = document.getElementById("main");
const form = document.getElementById("form");
const search = document.getElementById("search-box");
getMovies(API_URL);

/* apiden film verileri fetch yöntemi ile çekme */
function getMovies(url) {
  fetch(url)
    .then((res) => res.json())
    .then((data) => {
      console.log(data.results);
      showMovies(data.results);
    });
}

function showMovies(data) {
  main.innerHTML = "";
  data.forEach((movie) => {
    const { title, poster_path, vote_average, overview, id } = movie;
    const movieEl = document.createElement("div");
    movieEl.classList.add("movie");
    movieEl.innerHTML = `
        <img src="${IMG_URL + poster_path}"
                alt="${title}">
            <div class="movie-info">
                <h3> ${title} </h3>
                <span class="${getColor(vote_average)}"> ${vote_average.toFixed(
      2
    )} </span>
            </div>
            <div class="overview">
                <h3> Overview </h3>
                 ${overview.substring(0, 180)}...
                 <br/>
                 <button class="know-more" id="${id}"> Know More </button>
            </div>
        `;
    main.appendChild(movieEl);
    document.getElementById(id).addEventListener("click", () => {
      console.log(id);
      openNav(movie);
    });
  });
}

const overlayContent = document.getElementById("overlay-content");

/* Youtube linklerinin bulunduğu sayfayı açma */
function openNav(movie) {
  let id = movie.id;
  fetch(BASE_URL + "/movie/" + id + "/videos?" + API_KEY)
    .then((res) => res.json())
    .then((videoData) => {
      console.log(videoData);
      if (videoData) {
        document.getElementById("myNav").style.width = "100%";
        if (videoData.results.length > 0) {
          var embed = [];
          videoData.results.forEach((video) => {
            let { name, key, site } = video;
            if (site == "YouTube") {
              embed.push(`
              <iframe width="560" height="315" src="https://www.youtube.com/embed/${key}" 
              title="${name}" class="embed hide" frameborder="0" allow="accelerometer; autoplay; 
              clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
              allowfullscreen></iframe>
              `);
            }
          });
          overlayContent.innerHTML = embed.join("");
        } else {
          overlayContent.innerHTML = `<h1> No Results Found" </h1>`;
        }
      }
    });
  document.getElementById("myNav").style.width = "100%";
}

/* Youtube navbarı kapatma */
function closeNav() {
  document.getElementById("myNav").style.width = "0%";
}



/* Rating değerleri renklendirme */
function getColor(vote) {
  if (vote >= 8) {
    return "green";
  } else if (vote > 5) {
    return "yellow";
  } else {
    return "red";
  }
}

/* girilen ifadeye göre film arama işlemi */
form.addEventListener("submit", (e) => {
  e.preventDefault();
  const searchTerm = search.value;

  if (searchTerm) {
    getMovies(searchURL + "&query=" + searchTerm);
  } else {
    getMovies(API_URL);
  }
});
