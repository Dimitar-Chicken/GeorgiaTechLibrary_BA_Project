using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GTL_Application.Services;
using System.Diagnostics.CodeAnalysis;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class CommandHandlersTests
    {
        CommandHandler commandHandler;
        CommandHandlerWithParameters<bool> commandHandlerWithParameters;

        [Fact]
        public void TC015_CommandHandler_CanExecute_Passes()
        {
            bool result = false;

            commandHandler = new CommandHandler(() => result = true, () => result = true);

            commandHandler.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void TC015_CommandHandler_Execute_Passes()
        {
            bool result = false;
            
            commandHandler = new CommandHandler(() => result = true, () => result = true);

            commandHandler.Execute(null);

            Assert.True(result);
        }

        [Fact]
        public void TC015_CommandHandlerWithParameters_CanExecute_Passes()
        {
            bool result = false;

            commandHandlerWithParameters = new CommandHandlerWithParameters<bool>((a) => result = true, () => result = true);

            commandHandlerWithParameters.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void TC015_CommandHandlerWithParameters_Execute_Passes()
        {
            bool result = false;

            commandHandlerWithParameters = new CommandHandlerWithParameters<bool>((var) => result = var, () => result = true);

            commandHandlerWithParameters.Execute(true);

            Assert.True(result);
        }
    }
}
