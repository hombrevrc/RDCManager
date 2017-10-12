﻿using Caliburn.Micro;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<RDCSelectedMessage>
    {
        public IScreen RDCSessionVM
        {
            get;
            private set;
        }

        public IScreen RDCCollectionVM
        {
            get;
            private set;
        }

        public IScreen RDCSettingsVM
        {
            get;
            private set;
        }

        private readonly IEventAggregator _events;

        public ShellViewModel(IEventAggregator events, RDCSessionViewModel rdcSessionVM, RDCCollectionViewModel rdcCollectionVM, RDCSettingsViewModel rdcSettingsVM)
        {
            DisplayName = "RDC Manager";

            _events = events;
            _events.Subscribe(this);

            RDCSessionVM = rdcSessionVM;
            RDCCollectionVM = rdcCollectionVM;
            RDCSettingsVM = rdcSettingsVM;

            ShowRDCList();
        }

        public void ShowRDCList()
        {
            if (!(ActiveItem is RDCCollectionViewModel))
            {
                ChangeActiveItem(RDCCollectionVM, false);
            }
        }

        public void ShowRDCSession()
        {
            if (!(ActiveItem is RDCSessionViewModel))
            {
                ChangeActiveItem(RDCSessionVM, false);
            }
        }

        public void StopCurrentRDC()
        {
            _events.PublishOnUIThread(new StopRDCMessage());
        }

        public void ShowSettings()
        {
            if (!(ActiveItem is RDCSettingsViewModel))
            {
                ChangeActiveItem(RDCSettingsVM, false);
            }
        }

        public void Handle(RDCSelectedMessage message)
        {
            ShowRDCSession();
        }
    }
}