namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

public interface ManipulacaoTarefa{
    public void CadastrarTarefa(){}
    public void ListarTarefa(){}
    public void DeletarTarefa(){}
    public void AlterarTarefa(){}    
}