using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class PeriodicDeclarationRequest
    {
        private PeriodicDeclaration _periodicDeclaration;
        public PeriodicDeclarationRequest()
        {
            _periodicDeclaration = new PeriodicDeclaration();
        }

        public PeriodicDeclarationRequest(PeriodicDeclaration periodicDeclaration)
        {
            _periodicDeclaration = new PeriodicDeclaration();
            _periodicDeclaration = periodicDeclaration;
        }

        public PeriodicDeclarationResponse AddTaskForPeriodicDeclaration()
        {
            UserRepository periodicDeclarationRepos = new UserRepository();
            return periodicDeclarationRepos.AddTaskForPeriodicDeclaration(_periodicDeclaration);
        }

        public PeriodicDeclarationResponse GetPeriodicDeclaration()
        {
            UserRepository periodicDeclarationRepos = new UserRepository();
            return periodicDeclarationRepos.GetPeriodicDeclaration(_periodicDeclaration);
        }
    }
}