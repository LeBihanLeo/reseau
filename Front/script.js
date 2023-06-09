document.addEventListener('DOMContentLoaded', init, false);

function init(){
    //createChart()
    document.getElementById("whichServerStatsButton").addEventListener("click", ()=>callServiceServerStats());
    document.getElementById("AgeStatsButton").addEventListener("click", ()=>callServiceAgeStats());

    document.getElementById("securityButton").addEventListener("click", ()=>callSecurityStats());
    document.getElementById("securityButton").addEventListener("click", ()=>callTimeStats());

    document.getElementById("responseTimeButton").addEventListener("click", ()=>callTimeStats());

    

}

async function callTimeStats() {
    const response = await fetch("http://127.0.0.1:8080/api/time")
        .then(response => response.json())
        .then(data => {
            let jsonData = JSON.parse(data);
            console.log(jsonData)
            let keys = Object.keys(jsonData)
            let values = Object.values(jsonData)
            console.log(values);
            createTimeChart(keys, convertSecond(values))
        })
        .catch(error => {
            console.error(error);
        });
}

async function callSecurityStats() {
    const response = await fetch("http://127.0.0.1:8080/api/security")
        .then(response => response.json())
        .then(data => {
            let jsonData = JSON.parse(data);
            console.log(jsonData)
            createStatsChart(["Present", "Non-present"], [jsonData[0], jsonData[2]-jsonData[0]], "Serveur avec X-Content-Type-Options","xContentChart")
            createStatsChart(["Present", "Non-present"], [jsonData[1], jsonData[2]-jsonData[1]], "Serveur avec Strict-Transport-Security","strictChart")
        })
        .catch(error => {
            console.error(error);
        });
}
async function callHeader() {
    const response = await fetch("http://127.0.0.1:8080/api/header")
        .then(response => response.json())
        .then(data => {
            let jsonData = JSON.parse(data);
            console.log(jsonData)
        })
        .catch(error => {
            console.error(error);
        });
}


async function callServiceServerStats() {
    const response = await fetch("http://127.0.0.1:8080/api/server")
        .then(response => response.json())
        .then(data => {
            let jsonData = JSON.parse(data);
            console.log(jsonData)
            createStatsChart(Object.keys(jsonData), Object.values(jsonData), "Serveur statistique","myChart")
        })
        .catch(error => {
            console.error(error);
        });
}

async function callServiceAgeStats() {
    const response = await fetch("http://127.0.0.1:8080/api/age")
        .then(response => response.json())
        .then(data => {
            let jsonData = JSON.parse(data);
            let average = convertTime(jsonData.average);
            let deviation = convertTime(jsonData.deviation)
            console.log("Average = " + average)
            console.log("deviation = " + deviation)
            let agesDiv = document.getElementById("ages");
            agesDiv.innerHTML += "<p><b>Moyenne: </b>"+average+"</p>\n" + "<p><b>Ecart-type: </b>"+deviation+"</p>"
        })
        .catch(error => {
            console.error(error);
        });
}

function createTimeChart(labelsIn, dataIn){
    const ctx = document.getElementById("timeChart");
    let len = labelsIn.length;
    let colors = genColors(len)
    const data = {
        labels: labelsIn,
        datasets: [{
            label: 'Response time',
            data: dataIn,
            backgroundColor: colors,
            borderColor: colors,
            borderWidth: 1
        }]
    };
    const config = {
        type: 'bar',
        data: data,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        },
    };
    new Chart(ctx, config);
}

function createStatsChart(labelsChart, dataChart, title, divId ){
    let len = labelsChart.length;
    let colors = genColors(len)
    const ctx = document.getElementById(divId);
    const data = {
        labels:labelsChart,
        datasets: [{
            label: title,
            data: dataChart,
            backgroundColor: colors,
            hoverOffset: 4
        }]
    };
    const config = {
        type: 'doughnut',
        data: data,
    };
    new Chart(ctx, config);
}

function genColors(x) {
    const colors = [];

    for (let i = 0; i < x; i++) {
        const r = Math.floor(Math.random() * 256);
        const g = Math.floor(Math.random() * 256);
        const b = Math.floor(Math.random() * 256);
        const color = `rgb(${r}, ${g}, ${b})`;
        colors.push(color);
    }

    return colors;
}

function convertTime(timeString) {
    const timeParts = timeString.split(":");
    const hours = timeParts[0];
    const minutes = timeParts[1];
    const seconds = Math.floor(parseFloat(timeParts[2]));

    return `${hours} heures ${minutes} minutes ${seconds} secondes`;
}

function convertSecond(values){
    const valeurs = values.map((temps) => {
        const tempsEnMs = temps.slice(9);
        const tempsEnSec = temps.slice(0, 8).split(':').reduce((acc, val) => acc * 60 + +val, 0);
        return parseFloat(tempsEnMs) + tempsEnSec * 1000;
    });
    console.log(valeurs)
    return valeurs;
}