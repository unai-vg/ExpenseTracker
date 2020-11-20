using Blazored.Toast.Services;
using ExpenseTracker.Client.Data;

namespace ExpenseTracker.Client.Services
{
    public class Toaster
    {
        private readonly IToastService toaster;

        public Toaster(IToastService toaster)
        {
            this.toaster = toaster;
        }

        public void Toast(Bread bread)
        {
            switch (bread.Level)
            {
                case Bread.Hawtness.Success:
                    toaster.ShowSuccess(bread.Banner, bread.Message);
                    return;

                case Bread.Hawtness.Error:
                    toaster.ShowError(bread.Banner, bread.Message);
                    return;
            }
        }
    }
}
