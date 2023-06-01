namespace Api_TodoList.Models;

public class ApiResponse
{
    public bool error { get; set; }
    public string mensaje { get; set; } = string.Empty;
    public object? datos { get; set; } = new();

    public ApiResponse(){

    }
    public ApiResponse(bool error, string mensaje, object? datos){
        this.error = error;
        this.mensaje = mensaje;
        this.datos = datos;
    }
}

