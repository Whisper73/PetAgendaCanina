
var oTabla = $("#tblClientes").DataTable();
var oTablaTel = $("#tblTelefonos").DataTable();
jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    $("#btnTelefonos").on("click", function () {
        LlenarInformacionTelefonos();
    });
    $("#bntInsertar").on("click", function () {
        EjecutarComandos("POST");
    });
    $("#btnActualizarTel").on("click", function () {
        EjecutarComandos("PUT");
    });
    $("#btnEliminarTel").on("click", function () {
        EjecutarComandos("DELETE");
    });
    $("#btnCerrarModal").on("click", function () {
        LlenarTablaClientes();
    });
    $("#tblClientes tbody").on("click", 'tr', function () {
        //Levanta el evento del click sobre la tabla
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            EditarFila($(this).closest('tr'));
        }
    });
    $("#tblTelefonos tbody").on("click", 'tr', function () {
        //Levanta el evento del click sobre la tabla
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            EditarFilaTel($(this).closest('tr'));
        }
    });
    LlenarTablaClientes();
    /*LlenarCombotTipoTelefono();*/
});
function LlenarTablaClientes() {

    LlenarTablasXServicio("http://localhost:17036/Clientes", "#tblClientes");
}
//function LlenarCombotTipoTelefono() {
//    LlenarComboXServicios("http://localhost:53634/api/TipoTelefonos", "#cboTipoTelefono");
//}
//function EditarFilaTel(DatosFila) {
//    $("#txtCodigo").val(DatosFila.find('td:eq(0)').text());
//    $("#cboTipoTelefono").val(DatosFila.find('td:eq(1)').text());
//    $("#txtNumero").val(DatosFila.find('td:eq(3)').text());
//}
function EditarFila(DatosFila) {
    $("#txtDocumento").val(DatosFila.find('td:eq(2)').text());
    $("#txtNombre").val(DatosFila.find('td:eq(3)').text());
    $("#txtPrimerApellido").val(DatosFila.find('td:eq(4)').text());
    $("#txtSegundoApellido").val(DatosFila.find('td:eq(5)').text());
    $("#txtDireccion").val(DatosFila.find('td:eq(6)').text());
    $("#txtEmail").val(DatosFila.find('td:eq(7)').text());
    var Fecha = DatosFila.find('td:eq(8)').text();
    $("#txtFechaNacimiento").val(Fecha.split('T')[0]);
}
//function LlenarInformacionTelefonos() {
//    oTablaTel.clear().draw(false);
//    let Documento = $("#txtDocumento").val();
//    let Nombre = $("#txtNombre").val();
//    let PrimerApellido = $("#txtPrimerApellido").val();
//    let SegundoApellido = $("#txtSegundoApellido").val();

//    $("#txtNombreCliente").val(Nombre + " " + PrimerApellido + " " + SegundoApellido);
//    LlenarTablasXServicio("http://localhost:53634/api/Telefono?Documento=" + Documento, "#tblTelefonos");
//}
async function EjecutarComandos(Comando) {
    event.preventDefault();

    let Documento = $("#txtDocumento").val();
    let Nombre = $("#txtNombre").val();
    let Apellido = $("#txtApellido").val();
    let Num_Telefono = $("#txtTelefono").val();
    let Correo = $("#txtEmail").val();
    let Direccion = $("#txtDireccion").val();
    let Nivel = $("#txtNivel").val();

    debugger;

    let Cliente = {
        Documento: Documento,
        Nombre: Nombre,
        Apellido: Apellido,
        Num_Telefono: Num_Telefono,
        Correo: Correo,
        Direccion: Direccion,
        Nivel: Nivel
    }

    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("http://localhost:17036/Clientes",
            {
                method: Comando,
                mode: "cors",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(Cliente)
            });
        const Rpta = await Respuesta.json();
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").html(Rpta);
        LlenarInformacionTelefonos();
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").html(error);
    }
}

async function LlenarTablasXServicio(urlServicio, TablaLlenar) {
    //Invocar el servicio que trae la lista con la información para llenar la tabla
    try {
        const Respuesta = await fetch(urlServicio,
            {
                method: "GET",
                mode: "cors",
                headers: {
                    'Content-Type': 'application/json' 
                }
            });
        const Rpta = await Respuesta.json();

        //En Rpta, está el listado 
        //Recorrer la lista para llenar la tabla con encabezado y datos
        var Columnas = [];
        NombresColumna = Object.keys(Rpta[0]);
        for (var i in NombresColumna) {
            Columnas.push({
                data: NombresColumna[i],
                title: NombresColumna[i]
            });
        }
        //Llena los datos de la tabla
        $(TablaLlenar).DataTable({
            data: Rpta,
            columns: Columnas,
            destroy: true
        });
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").html(error);
    }
}