using BusinessLogic.Interfaces;
using BusinessLogicPOC;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class BusinessLogicTest
    {
        IBusinessLogic _businessLogic;
        [TestInitialize]
        public void Init()
        {
            _businessLogic = new BusinessLogicPOC.BusinessLogic();
        }
        [TestMethod]
        public void StartGame_GameStartedState_ReturnsTrue()
        {
            _businessLogic.StartGame();
            Assert.IsTrue(_businessLogic.GameStartedState);
        }
        [TestMethod]
        public void StartGame_GameStateChanged_Invoked()
        {
            bool called = false;
            _businessLogic.GameStateChanged += (state) => { called = true; };
            _businessLogic.StartGame();
            Assert.IsTrue(called);
            
        }
    }
}