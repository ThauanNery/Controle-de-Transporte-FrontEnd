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
        
        /*Instituição*/
        public const string InstituicaoGetAll = "Instituicao/GetAll";
        public const string InstituicaoGetById = "Instituicao/GetById/{id}";
        public const string InstituicaoCreate = "Instituicao/Create";
        public const string InstituicaoUpdate = "Instituicao/Update/{id}";
        public const string InstituicaoDelete = "Instituicao/Delete/{id}";
    }
}
