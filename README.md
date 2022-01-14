[![CircleCI](https://circleci.com/gh/pauloprsdesouza/personal-portfolio-api.svg?style=svg)](https://circleci.com/gh/pauloprsdesouza/personal-portfolio-api)

# A REST Api for my personal-portfolio

This REST Api is the Backed for [personal-porftfolio](https://github.com/pauloprsdesouza/personal-portfolio) frontend.

## Used Stacks 

- dotNet Core 3.1
- AWS DynamoDB (NoSQL)
- AWS Lambda Function
- AWS API Gateway
- AWS CloudFormation

## A basic Infrastructure as a Code using AWS Cloud Formation

So, this code allows you to realize an automated deployment using AWS CLI.

``` yaml
AWSTemplateFormatVersion: 2010-09-09

Transform: AWS::Serverless-2016-10-31

Description: Personal Portfolio

Parameters:

  Environment:
    Type: String
    Description: Set the deployment environment.
    AllowedValues:
      - Development
      - Staging
      - Production

  JwtSecret:
    Type: String
    Description: Set secret token JWT

Mappings:
  StageNameMap:
    Development:
      StageName: dev
    Staging:
      StageName: stg
    Production:
      StageName: prod
  EnvironmentToPathBaseMap:
    Development:
      PathBase: /development
    Staging:
      PathBase: /staging
    Production:
      PathBase: /

Globals:

  Api:
    OpenApiVersion: 3.0.1

Resources:

  Lambda:
    Type: AWS::Serverless::Function
    Properties:
      FunctionName: portfolio-api
      Handler: Portfolio.Api::Portfolio.Api.LambdaEntryPoint::FunctionHandlerAsync
      Runtime: dotnetcore3.1
      MemorySize: 1024
      Timeout: 30
      Environment:
        Variables:
          ASPNETCORE_ENVIRONMENT: !Ref Environment
      Events:
        AnyHttpRequest:
          Type: Api
          Properties:
            Path: "/{proxy+}"
            Method: ANY
            RestApiId: !Ref ApiGateway
      Role: !Sub arn:aws:iam::${AWS::AccountId}:role/AppLambdaExecutionRole
      Tags:
          Name: portfolio-api:Lambda

  ApiGateway:
    Type: AWS::Serverless::Api
    Properties:
      StageName: !FindInMap [ StageNameMap, !Ref Environment, StageName ]
      Name: portfolio-api
      Tags:
          Name: portfolio-api:ApiGateway

  DynamoDBTable:
    Type: AWS::DynamoDB::Table
    DeletionPolicy: Retain
    Properties:
      TableName: portfolio-api
      KeySchema:
        - AttributeName: PK
          KeyType: HASH
        - AttributeName: SK
          KeyType: RANGE
      AttributeDefinitions:
        - AttributeName: PK
          AttributeType: S
        - AttributeName: SK
          AttributeType: S
      BillingMode: PAY_PER_REQUEST
      Tags:
        - Key: Name
          Value: portfolio-api:DynamoDB

```
