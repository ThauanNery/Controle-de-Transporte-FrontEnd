namespace Controle_de_Transporte_FrontEnd.Rotas
{
    public class RouteConst
    {
      
        /*Cargo*/
        public const string CargoGetAll = "Cargo/GetAll";
        public const string CargoGetById = "Cargo/GetById/{id}";
        public const string CargoCreate = "Cargo/Create";
        public const string CargoUpdate = "Cargo/Update/{id}";
        public const string CargoDelete = "Cargo/Delete/{id}";
        
        /*Departamento*/
        public const string DepartamentoGetAll = "Departamento/GetAll";
        public const string DepartamentoGetById = "Departamento/GetById/{id}";
        public const string DepartamentoCreate = "Departamento/Create/{institucaoId}";
        public const string DepartamentoUpdate = "Departamento/Update/{id}";
        public const string DepartamentoDelete = "Departamento/Delete/{id}";

        /*Funcionarios*/
        public const string FuncionariosGetAll = "Funcionarios/GetAll";
        public const string FuncionariosGetById = "Funcionarios/GetById/{id}";
        public const string FuncionariosCreate = "Funcionarios/Create/{cargoId},{departamentoId}";
        public const string FuncionariosUpdate = "Funcionarios/Update/{id}";
        public const string FuncionariosDelete = "Funcionarios/Delete/{id}";
        
        /*Instituição*/
        public const string InstituicaoGetAll = "Instituicao/GetAll";
        public const string InstituicaoGetById = "Instituicao/GetById/{id}";
        public const string InstituicaoCreate = "Instituicao/Create";
        public const string InstituicaoUpdate = "Instituicao/Update/{id}";
        public const string InstituicaoDelete = "Instituicao/Delete/{id}";
        
        /*Manutenção*/
        public const string ManutencaoGetAll = "Manutencao/GetAll";
        public const string ManutencaoGetById = "Manutencao/GetById/{id}";
        public const string ManutencaoCreate = "Manutencao/Create";
        public const string ManutencaoUpdate = "Manutencao/Update/{id}";
        public const string ManutencaoDelete = "Manutencao/Delete/{id}";

        /*Tipo de Transporte*/
        public const string TipoDeTransporteGetAll = "TipoDeTransporte/GetAll";
        public const string TipoDeTransporteGetById = "TipoDeTransporte/GetById/{id}";
        public const string TipoDeTransporteCreate = "TipoDeTransporte/Create";
        public const string TipoDeTransporteUpdate = "TipoDeTransporte/Update/{id}";
        public const string TipoDeTransporteDelete = "TipoDeTransporte/Delete/{id}";

        /*Usuarios*/
        public const string BuscarPorLogin = "Usuario/BuscarPorLogin/{login}";
        public const string UsuarioGetAll = "Usuario/GetAll";
        public const string UsuarioGetById = "Usuario/GetById/{id}";
        public const string UsuarioCreate = "Usuario/Create/{funcionarioId}";
        public const string UsuarioUpdate = "Usuario/Update/{id}";
        public const string UsuarioDelete = "Usuario/Delete/{id}";
    }
}
