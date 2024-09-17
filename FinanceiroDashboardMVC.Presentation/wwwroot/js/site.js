
$(document).ready(function () {
    // Dados fictícios para os gráficos
    var inadimplenciaData = [12, 19, 3, 5, 2, 3];
    var receitaData = [3, 7, 15, 10, 8, 5];

    // Gráfico de Inadimplência
    var ctxInadimplencia = document.getElementById('inadimplenciaChart').getContext('2d');
    new Chart(ctxInadimplencia, {
        type: 'line',
        data: {
            labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
            datasets: [{
                label: 'Inadimplência',
                data: inadimplenciaData,
                borderColor: 'rgba(220,53,69,1)',
                backgroundColor: 'rgba(220,53,69,0.2)',
                fill: true,
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Gráfico de Receita
    var ctxReceita = document.getElementById('receitaChart').getContext('2d');
    new Chart(ctxReceita, {
        type: 'bar',
        data: {
            labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
            datasets: [{
                label: 'Receita',
                data: receitaData,
                backgroundColor: 'rgba(40,167,69,0.6)',
                borderColor: 'rgba(40,167,69,1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});

$('#sidebarCollapse').on('click', function () {
    $('#sidebar').toggleClass('active');
});