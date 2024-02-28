---
slug: documenting-dotnet-api
title: Docummenting .NET API
authors: paulo_roberto_de_souza
tags: [swagger, redocly]
image: https://i.imgur.com/mErPwqL.png
enableComments: true
---

## Introduction

<div style={{ textAlign: 'justify' }}>
When we are developing software, more specifically an [Application Programing Interface (API)](https://aws.amazon.com/what-is/api/), we usually code based on our own ideas and understandings. But have you ever needed to consume an API in your life? I believe so! In such cases, if it has poor documentation or lacks documentation entirely, you wouldn't know where to start. The compass (documentation) is lost in this scenario, making any integration you had in mind nearly impossible to achieve.

However, there are some approaches available to help us in this hard task, you know? When the [Swagger](https://swagger.io/), now known as [OpenAPI](https://www.openapis.org/) Specification was created by [Tony Tam](https://www.linkedin.com/in/tonytam/) in 2011, which has the role to standardize the way APIs are described and documented, my entire life changed. I thought, ok! Now I can view my APIs in a human-readable way with descriptions and details about everything. This is amazing because it can possible to provide a communication with teams better, besides integrates with other APIs in a simple way. In addition, Swagger quickly gained popularity in the developer community for its ability to streamline the process of designing, building, testing, and documenting APIs. Its success led to the formation of the OpenAPI Initiative under the Linux Foundation in 2015, ensuring its development as an industry standard for API design. This transition marked a significant milestone in the evolution of API technology, highlighting Swagger's impact on improving interoperability and communication between different software systems.

After that, comunities developed different versions aiming to provide [UI](https://www.figma.com/resource-library/difference-between-ui-and-ux/) and [UX](https://www.figma.com/resource-library/difference-between-ui-and-ux/) better, as an example of [Redocly](https://redocly.com/), which is a software company that specializes in API documentation and management tools, harnessing the power of the OpenAPI Specification to streamline the lifecycle of API development. Founded on the principle that high-quality documentation is crucial for effective API utilization, Redocly provides solutions that facilitate the creation, maintenance, and deployment of API documentation, enhancing developer experience and promoting better integration practices. Their flagship product, [Redoc](https://github.com/Redocly/redoc), is an open-source tool that offers a visually appealing and interactive way for developers to browse and test API documentation. Beyond Redoc, Redocly's suite includes tools for validating, bundling, and managing API definitions, catering to the needs of both small and large organizations. By focusing on the usability and accessibility of API documentation, Redocly has become a pivotal player in the API community, helping businesses to maximize the potential of their APIs through better documentation and developer engagement.

If you've made it this far, let's dive into coding!

## Requisites
Now all you need to do is to install these following packages.
You can install it whatever you want, if you are using [Visual Studio](https://visualstudio.microsoft.com/pt-br/), you can install it by Nuget Package Manager or even by the terminal. But if you are using [VsCode](https://code.visualstudio.com/) put the above code in the terminal and enjoy 😄.

### Swagger
You can found it in this [Nuget Link](https://www.nuget.org/packages/swashbuckle.aspnetcore.swagger/).

```javascript
dotnet add package Swashbuckle.AspNetCore.Swagger --latest
```

### Redoc
 You can found it in this [Nuget Link](https://www.nuget.org/packages/Swashbuckle.AspNetCore.ReDoc).

```javascript
dotnet add package Swashbuckle.AspNetCore.ReDoc --latest
```


## Let's to hands on: Creating a .Net Web Api
Let's create our webapi applying the following code below. There are other templates that you can se [here](https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet-new).

```javascript
dotnet new webapi –name MyFirstWebAPIProject
```

### The extension method

To provide a reusability and manutenibility, regarding the Single Resposibility Principle, I created this extension method and divided it into three parts. Firstly, we need to create a *static* class named **SwaggerConfiguration**, and then create these properties just like that or you can use the variable names that are most suitble for you.

```csharp showLineNumbers
private const string Title = "<YOUR API NAME>";
private const string Description = "<YOUR API DESCRIPTION>";
private const string Version = "<VERSION> eg. v1";
```

The following code allows registering the necessary configuration into the services and their corresponding implementation types into a DI container. When an application starts, this container is used to instantiate and provide instances of these services wherever they are needed, thus facilitating the inversion of control.

```csharp showLineNumbers
public static void AddSwaggerDocumentation(this IServiceCollection services)
{
	_ = services.AddSwaggerGen(options =>
	{
		string xmlComments = Path.Combine(AppContext.BaseDirectory, "<YOUR.NAMESPACE.API>.xml");

		options.SwaggerDoc(Version, new OpenApiInfo
		{
			Title = Title,
			Description = Description,
			Version = Version
		});

		options.AddSecurityDefinition("api-key", new OpenApiSecurityScheme
		{
			Type = SecuritySchemeType.ApiKey,
			Name = "Authorization",
			In = ParameterLocation.Header
		});

		options.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Id = "api-key",
						Type = ReferenceType.SecurityScheme
					}
				},
				new[] { "readAccess", "writeAccess" }
			}
		});

		options.IncludeXmlComments(xmlComments);
	});
}
```

Here is the same case, but we are registering in the middleware pipeline. Many middleware components are configured through extension methods on IApplicationBuilder. These methods typically follow the naming pattern UseXYZ, where "XYZ" is the name of the middleware. In our example, app.UseSwagger() and app.UseReDoc() configures the application to serve static files from the web root folder.

```csharp showLineNumbers
public static void UseSwaggerDocumentation(this IApplicationBuilder app)
{
	_ = app.UseSwagger(options =>
	{
		options.RouteTemplate = "docs/swagger/{documentname}/swagger.json";
	});

	_ = app.UseReDoc(options =>
	{
		options.DocumentTitle = Title;
		options.RoutePrefix = "docs";
		options.SpecUrl($"swagger/{Version}/swagger.json");
	});

	_ = app.UseSwagger(options =>
	{
		options.RouteTemplate = "swagger/swagger/{documentname}/swagger.json";
	});

	_ = app.UseSwaggerUI(options =>
	{
		options.DocumentTitle = Title;
		options.RoutePrefix = "swagger";
		options.SwaggerEndpoint($"swagger/{Version}/swagger.json", Description);
	});
}
```

> ⚠️Important! Now that almost everything is configured, open the **statup project .csproj file**, which probably is the host containing controllers, Program.cs, etc. and then add the properties below into the ``<PropertyGroup>``.
> ```cs
> <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
> <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
> <GenerateDocumentationFile>true</GenerateDocumentationFile>

Finally, let's to call the extension methods in the Program.cs just like that, where services is **IServiceCollection** and app is **WebApplicationBuilder**.

```cs
services.AddSwaggerDocumentation();
app.UseSwaggerDocumentation();
```

### Docummenting the first enpoint

```cs
/// <summary>
/// Decribes what the endpoint does
/// </summary>
[HttpPost]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
public async Task<IActionResult> MyMethod()
{
    ...
    return Ok(...);
}
```

### Running the app and voilà

## Conclusion


## References


</div>
---