window.onload = function () {
    ListarTrabajadorBandeja();
}

function ListarTrabajadorBandeja() {
    $("#divBandeja").load("_BandejaTrabajador");
}

function FiltroSexo() {
    let filtro = $("#cboFiltroSexo").val();
    $("#divBandeja").load("_FiltroSexoTrabajador/?sexo=" + filtro);
}