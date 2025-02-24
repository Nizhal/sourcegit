using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Avalonia.Threading;

using CommunityToolkit.Mvvm.ComponentModel;

namespace SourceGit.ViewModels
{
    public class Popup : ObservableValidator
    {
        public object View
        {
            get;
            set;
        } = null;

        public bool InProgress
        {
            get => _inProgress;
            set => SetProperty(ref _inProgress, value);
        }

        public string ProgressDescription
        {
            get => _progressDescription;
            set => SetProperty(ref _progressDescription, value);
        }

        public bool CanCancelInProgress
        {
            get;
            set;
        } = false;

        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode")]
        public bool Check()
        {
            if (HasErrors)
                return false;
            ValidateAllProperties();
            return !HasErrors;
        }

        public void CancelInProgress()
        {
            if (CanCancelInProgress)
                DoCancelInProgress();
        }

        public virtual bool CanStartDirectly()
        {
            return true;
        }

        public virtual Task<bool> Sure()
        {
            return null;
        }

        public virtual void DoCancelInProgress()
        {
        }

        protected void CallUIThread(Action action)
        {
            Dispatcher.UIThread.Invoke(action);
        }

        protected void SetProgressDescription(string description)
        {
            CallUIThread(() => ProgressDescription = description);
        }

        private bool _inProgress = false;
        private string _progressDescription = string.Empty;
    }
}
