window.onload = function () {
    ListarTrabajador();
    CargarDepartamento();
}

function ListarTrabajador() {
    $("#divTrabajador").load("_ListarTrabajador");
}

function ochDepartamento() {
    let idDepartamento = $("#cboDepartamento").val();
    CargarProvincia(idDepartamento);
    CargarDistrito(0);
}

function ochProvincia() {
    let idProvincia = $("#cboProvincia").val();
    CargarDistrito(idProvincia);
}

function CargarDepartamento() {
    fetchGet("/Trabajador/CargarDepartamento", function (data) {
        llenarCombo(data, "cboDepartamento", "nombreDepartamento", "id", "0");
    });
}

function CargarProvincia(idDepartamento) {
    fetchGet("/Trabajador/CargarProvincia/?idDepartamento=" + idDepartamento, function (data) {
        llenarCombo(data, "cboProvincia", "nombreProvincia", "id", "0");
    });
}

function CargarDistrito(idProvincia) {
    fetchGet("/Trabajador/CargarDistrito/?idProvincia=" + idProvincia, function (data) {
        llenarCombo(data, "cboDistrito", "nombreDistrito", "id", "0");
    });
}

function FiltroSexo() {
    let filtro = $("#cboFiltroSexo").val();
    $("#divTrabajador").load("_FiltroSexoTrabajador/?sexo=" + filtro);
}

function NuevoTrabajador() {
    document.getElementById("staticBackdropLabel").innerHTML = "Nuevo Trabajador";
    LimpiarTrabajador();
    CargarDepartamento();
    CargarProvincia(0);
    CargarDistrito(0);
}

function GuardarTrabajador() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarLongitudMaxima("frmTrabajador");
    if (error != "") {
        Error1(error);
        return;
    }

    var frmTrabajador = document.getElementById("frmTrabajador");
    var frm = new FormData(frmTrabajador);
    var valor = $("#id").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Trabajador?", function () {
            fetchPostText("/Trabajador/GuardarTrabajador", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarTrabajador").click();
                    ListarTrabajador();
                    LimpiarTrabajador();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarTrabajador();
                    Error1("Hubo un error al guardar el Trabajador.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Trabajador?", function () {
            fetchPostText("/Trabajador/GuardarTrabajador", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarTrabajador").click();
                    ListarTrabajador();
                    LimpiarTrabajador();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarTrabajador();
                    Error1("Hubo un error al guardar los cambios del Trabajador.");
                }
            })
        })
    }
}

function LimpiarTrabajador() {
    LimpiarDatos("frmTrabajador");
}

function EditarTrabajador(idTrabajador) {
    LimpiarTrabajador();
    setTimeout(function () {
        fetchGet("/Trabajador/RecuperarTrabajador/?idTrabajador=" + idTrabajador, function (data1) {
            fetchGet("/Trabajador/CargarProvincia/?idDepartamento=" + data1["idDepartamento"], function (data2) {
                llenarCombo(data2, "cboProvincia", "nombreProvincia", "id", "0");
                fetchGet("/Trabajador/CargarDistrito/?idProvincia=" + data1["idProvincia"], function (data3) {
                    llenarCombo(data3, "cboDistrito", "nombreDistrito", "id", "0");
                    Complemento(idTrabajador);
                });
            });
        });
    }, 300)
}

function Complemento(idTrabajador) {
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Trabajador";
    setTimeout(recuperarGenericoEspecifico("/Trabajador/RecuperarTrabajador/?idTrabajador=" + idTrabajador,
        "frmTrabajador", [], false), 200);
    //inFocusName("nombre", 470);
}

function EliminarTrabajador(idTrabajador) {
    Confirmacion("¿Desea eliminar el Trabajador?", "Confirmar eliminación", function (res) {
        fetchGetText("/Trabajador/EliminarTrabajador/?idTrabajador=" + idTrabajador, function (rpta) {
            if (rpta == "1") {
                ListarTrabajador();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarTrabajador();
                Error1("Hubo un error al eliminar el Trabajador.");
            }
        })
    })
}

function ValidarCampos() {
    let nombre = getN("nombres");
    let tipoDocumento = getN("tipoDocumento");
    let numeroDocumento = getN("numeroDocumento");
    let sexo = getN("sexo");
    let idDepartamento = getN("idDepartamento");
    let idProvincia = getN("idProvincia");
    let idDistrito = getN("idDistrito");
    if (nombre != "" && nombre != undefined) {
        if (tipoDocumento != "" && tipoDocumento != undefined) {
            if (numeroDocumento != "" && numeroDocumento != undefined) {
                if (sexo != "" && sexo != undefined) {
                    if (idDepartamento != "0" && idDepartamento != undefined) {
                        if (idProvincia != "0" && idProvincia != undefined) {
                            if (idDistrito != "0" && idDistrito != undefined) {
                                return true;
                            }
                            else {
                                Error1("Seleccione un Distrito.");
                                return false;
                            }
                        }
                        else {
                            Error1("Seleccione una Provincia.");
                            return false;
                        }
                    }
                    else {
                        Error1("Seleccione un Departamento.");
                        return false;
                    }
                }
                else {
                    Error1("Seleccione un Sexo.");
                    return false;
                }
            }
            else {
                Error1("Ingrese el N° De Documento.");
                return false;
            }
        }
        else {
            Error1("Seleccione el Tipo Documento.");
            return false;
        }
    }
    else {
        Error1("Ingrese el Nombre.");
        return false;
    }
}