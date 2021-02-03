dotnet test CoursesTests.sln /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=TestResults/Coverage/

cd CoursesTests.Ordering.Domain.Tests

dotnet reportgenerator -reports:../**/coverage.cobertura.xml -targetdir:../TestResults/Report -reporttypes:"HtmlInline_AzurePipelines;Cobertura"