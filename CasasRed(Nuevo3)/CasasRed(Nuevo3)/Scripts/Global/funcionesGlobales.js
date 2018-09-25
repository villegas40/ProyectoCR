﻿
function imprimirBusqueda(cabecero, cuerpo, title) {
    var invisible = document.getElementsByClassName("InvisiblePrint");
    var inputs = document.getElementsByTagName('input');
    var selects = document.getElementsByTagName('select');
    for (var i = 0; i < invisible.length; i++) {
        invisible[i].style.display = "none";
    }
    for (var l = 0; l < inputs.length; l++) {
        inputs[l].setAttribute('value', inputs[l].value);
        inputs[l].setAttribute("readonly", true);
    }
    for (var o = 0; o < selects.length; o++) {
        selects[o].setAttribute("disabled", true);
    }


    var titulo = title.replace(/\s/gm, '-');
    $.when(generarReporte('/Reportes/Index', titulo, cuerpo.innerHTML, cabecero.innerHTML)).then(function () {
        for (var i = 0; i < invisible.length; i++) {
            invisible[i].style.removeProperty("display");
        }
        for (var l = 0; l < inputs.length; l++) {
            inputs[l].removeAttribute("readonly");
        }
        for (var o = 0; o < selects.length; o++) {
            selects[o].removeAttribute("disabled");
        }
    });
}

function generarReporte(url, nombre, contenido, cabecero) {
    var d1 = $.Deferred();
    printWindow = window.open(url + '?titulo=' + nombre, '', 'height=400,width=800');
    printWindow.addEventListener('load', function (e) {
        d1.resolve();
        $(printWindow.document).ready(function () {
            var cabecerofinal = printWindow.document.body.children[0].children[0].children[2].innerHTML + cabecero;
            $(printWindow.document.body.children[0].children[0].children[2]).html(cabecerofinal);
            $(printWindow.document.body.children[0].children[1]).html(contenido);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        });
    }, true);
    return d1.promise();
}