$(document).ready(function () {
    LoadGameGenres();
    LoadOtherGenres();
})

function RenderGameGenres(genres, container) {
    container.empty();

    for (const genre of genres) {
        container.append(`
        <div class="card mb-3 m-3" style="max-width: 10rem;">
            <div class="card-body">
                <h5 class="card-title">${genre.name}</h5>
                <a class="btn btn-danger" onclick="RemoveGenre('${genre.encodedName}')">Remove</a>
            </div>
        </div>`)
    }
}

function RemoveGenre(genreEncodedName) {
    const container = $("#genres");
    const gameEncodedName = container.data("encodedName");

    $.ajax({
        url: "/WebGames/RemoveGenreFromGame/" + genreEncodedName + "/" + gameEncodedName,
        type: "POST",
        async: true,
        success: function (msg) {
            toastr["info"]("Removed genre " + genreEncodedName);
            LoadGameGenres();
            LoadOtherGenres();
        },
        error: function () {
            toastr["error"]("Something went wrong when removing genre.");
        }
    });
}

function LoadGameGenres() {
    const container = $("#genres");
    const gameEncodedName = container.data("encodedName");

    $.ajax({
        url: `/WebGames/${gameEncodedName}/GetGenres`,
        type: "get",
        success: function (data) {
            if (!data.length) {
                container.html(`<div class="card mb-3 m-3" style="max-width: 25rem;">
            <div class="card-body">
                <h5 class="card-title">This game doesn't have any genres!</h5>
            </div>
        </div>`);
            }
            else {
                RenderGameGenres(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong when loading genres.");
        }
    })
}

function RenderGenresDropdown(genres, container) {
    container.empty();

    for (const genre of genres) {
        container.append(`
        <li><button type="button" class="dropdown-item" onclick="AddGenre('${genre.encodedName}', '${container.data("encodedName")}')">${genre.name}</button></li>`)
    }
}

function LoadOtherGenres() {
    const container = $("#genres-dropdown");
    const gameEncodedName = container.data("encodedName");

    $.ajax({
        url: `/WebGames/${gameEncodedName}/GetAllGenresExcludingExisting`,
        type: "get",
        success: function (data) {
            if (!data.length) {
                container.html(`<div class="card mb-3 m-3" style="max-width: 25rem;">
            <div class="card-body">
                <h5 class="card-title">There are no other genres.</h5>
            </div>
        </div>`);
            }
            else {
                RenderGenresDropdown(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong when loading genres not present in the current game.");
        }
    })
}

function AddGenre(genreEncodedName, gameEncodedName) {
    $.ajax({
        url: "/WebGames/AddGenreToGame/" + genreEncodedName + "/" + gameEncodedName,
        type: "POST",
        async: true,
        success: function (msg) {
            toastr["info"]("Added genre " + genreEncodedName + " to game " + gameEncodedName);
            LoadGameGenres();
            LoadOtherGenres();
        },
        error: function () {
            toastr["error"]("Something went wrong when adding genre.");
        }
    });
}