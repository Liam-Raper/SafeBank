using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace UIAutomationTests.Tests
{
    public class BaseUITest
    {

        protected static BrowserWindow Browser;
        protected static Process BrowserProcess;
        
        public BaseUITest()
        {
            Playback.Initialize();
            Playback.PlaybackSettings.DelayBetweenActions = 250;
        }
        
        [TestInitialize]
        public void TestInit()
        {
            if(BrowserProcess == null)
            {
                Browser = BrowserWindow.Launch(new Uri("http://localhost:10494/"));
                Browser.CloseOnPlaybackCleanup = false;
                BrowserProcess = Browser.Process;
            }
            Browser = BrowserWindow.FromProcess(BrowserProcess);
        }

        [ClassCleanup]
        public void CleanUp()
        {
            Browser.Close();
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

    }
}
