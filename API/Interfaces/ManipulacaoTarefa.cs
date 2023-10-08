namespace API;
using Microsoft.AspNetCore.Mvc;
using API.Data;

public interface ManipulacaoTarefa{
    public void CadastrarTarefaProjeto(){}
    public void CadastrarTarefaAtividade(){}
    public void ListarTarefaProjeto(){}
    public void ListarTarefaAtividade(){}
    public void DeletarTarefaProjeto(){}
    public void DeletarTarefaAtividade(){}
    public void AlterarTarefaProjeto(){}  
    public void AlterarTarefaAtividade(){}   
}