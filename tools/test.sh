#!/bin/bash

set -e

basedir="$(dirname $0)/.."
solution_dir="$basedir/src"
api_project_dir="Portfolio.Api"
test_project_dir="Portfolio.Tests"

cd $solution_dir/$test_project_dir

dotnet test \
  /p:CollectCoverage="true" \
  /p:CoverletOutputFormat="opencover"

dotnet new tool-manifest
dotnet tool install --local dotnet-reportgenerator-globaltool

dotnet reportgenerator \
  "-reports:coverage/coverage.opencover.xml" \
  "-reporttypes:Html" \
  "-targetdir:coveragereport"
