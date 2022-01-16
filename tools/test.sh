#!/bin/bash

set -e

basedir="$(dirname $0)/.."
solution_dir="$basedir/src"
api_project_dir="Portfolio.Api"
test_project_dir="Portfolio.Tests"

cd $solution_dir/$test_project_dir

dotnet test \
  /p:AltCover="true" \
  /p:AltCoverForce="true" \
  /p:AltCoverOpenCover="true" \
  /p:AltCoverXmlReport="coverage/opencover.xml" \
  /p:AltCoverInputDirectory="$api_project_dir" \
  /p:AltCoverAttributeFilter="ExcludeFromCodeCoverage" \
  /p:AltCoverAssemblyExcludeFilter="System(.*)|xunit|$test_project_dir|$api_project_dir.Views"

dotnet tool install --tool-path tools dotnet-reportgenerator-globaltool

./tools/reportgenerator \
  "-reports:coverage/opencover.xml" \
  "-reporttypes:Html;HtmlSummary" \
  "-targetdir:coverage/report"

ls
