﻿using System.Linq;
using System.Text;
using TechTalk.SpecFlow.TestProjectGenerator.Driver;

namespace TechTalk.SpecFlow.Specs.StepDefinitions.CucumberMessages
{
    [Binding]
    public class TestSuiteSteps
    {
        private readonly ProjectsDriver _projectsDriver;
        private readonly TestSuiteSetupDriver _testSuiteSetupDriver;

        public TestSuiteSteps(ProjectsDriver projectsDriver, TestSuiteSetupDriver testSuiteSetupDriver)
        {
            _projectsDriver = projectsDriver;
            _testSuiteSetupDriver = testSuiteSetupDriver;
        }

        [Given(@"there are (\d+) feature files")]
        public void GivenThereAreFeatureFiles(int featureFilesCount)
        {
            _testSuiteSetupDriver.AddFeatureFiles(featureFilesCount);
        }

        [Given(@"the cucumber implementation is (.*)")]
        public void GivenTheCucumberImplementationIs(string cucumberImplementation)
        {
            _testSuiteSetupDriver.AddFeatureFiles(1);
        }
    }

    public class TestSuiteSetupDriver
    {
        private readonly ProjectsDriver _projectsDriver;

        public TestSuiteSetupDriver(ProjectsDriver projectsDriver)
        {
            _projectsDriver = projectsDriver;
        }

        public void AddFeatureFiles(int count)
        {
            if (count == 0)
            {
                _projectsDriver.CreateProject("C#");
                return;
            }

            for (int n = 0; n < count; n++)
            {
                string featureTitle = $"Feature{n}";
                var featureBuilder = new StringBuilder();
                featureBuilder.AppendLine($"Feature: {featureTitle}");

                foreach (string scenario in Enumerable.Range(0, 1).Select(i => $"Scenario: passing scenario nr {i}\r\nWhen the step pass in {featureTitle}"))
                {
                    featureBuilder.AppendLine(scenario);
                    featureBuilder.AppendLine();
                }

                _projectsDriver.AddFeatureFile(featureBuilder.ToString());
            }
        }
    }
}
