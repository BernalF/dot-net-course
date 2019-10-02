
"use strict";

var body = document.body;

function fetchData() {
    var request = new XMLHttpRequest();
    request.open('GET', 'https://localhost:5001/api/products', true);

    request.onload = function () {
        if (request.status !== 200) {
            body.innerHTML = 'An error occurred during your request: ' + request.status + ' ' + request.statusText;
            return;
        }
        document.getElementById("hello").innerText = request.responseText;
    };
    request.onerror = function () {
        body.innerHTML = 'An error occurred during your request: ' + request.status + ' ' + request.statusText;
    };
    request.send();
}

document.onreadystatechange = function () {
    if (document.readyState === "interactive") {
        fetchData();
    }
};