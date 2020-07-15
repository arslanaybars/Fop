# Fop
#### Filtering, Ordering (Sorting) and Pagination library for .Net Core, ASP.NET Core, C#

For .Net Core developers, **Fop** serves you quite simple, easy to integrate and extensible Filtering, Ordering (Sorting) and Paging functionality.

[![Fop Nuget](https://img.shields.io/nuget/v/Fop)](https://www.nuget.org/packages/Fop)

To see detailed documentation please visit [wiki page](https://github.com/arslanaybars/Fop/wiki)

#### Quick Start
Let's see how easy to use **Fop**

 1. Install `Fop` NuGet package from [here](https://www.nuget.org/packages/Fop/).
 ````
PM> Install-Package Fop
````
2. Add FopQuery to your get method
```csharp
[HttpGet]
public async Task<IActionResult> Index([FromQuery] FopQuery request)
{
    var fopRequest = FopExpressionBuilder<Student>.Build(request.Filter, request.Order, request.PageNumber, request.PageSize);

    var (filteredStudents, totalCount) = await _studentRepository.RetrieveStudents(fopRequest);

    return Ok(filteredStudents);
}
```
3. ApplyFop from your repository
```csharp
 public async Task<(List<Student>, int)> RetrieveStudents(IFopRequest request)
 {
     var (filteredStudents, totalCount) = _context.Students.ApplyFop(request);
     return (await filteredStudents.ToListAsync(), totalCount);
 }
```
*Install Fop*, Build your object by using *FopExpressionBuilder<Student>.Build()* then *ApplyFop()* 
That's All 🤘  

More and more detail at here and in Wiki page. Please visit before you decided to not use

## Deep Dive
#### Fop Operators
Supported operators for type are below;

Fop uses these query sign for preparing expression. 

|Operators          |Query Sign  |Int/Long |String | Char |DateTime|
|-------------------|------------|---------|-------|------|--------|
|Equal              |`==`        | ✔️     | ✔️    | ✔️  | ✔️     |
|NotEqual           |`!=`        | ✔️     | ✔️    | ✔️  | ✔️     |
|GreaterThan        |`>`         | ✔️     | ❌    | ❌  | ✔️     |
|GreaterOrEqualThan |`>=`        | ✔️     | ❌    | ❌  | ✔️     |
|LessThan           |`<`         | ✔️     | ❌    | ❌  | ✔️     |
|LessOrEqualThan    |`<=`        | ✔️     | ❌    | ❌  | ✔️     |
|Contains           |`~=`        | ❌     | ✔️    | ❌  | ❌     | 
|NotContains        |`!~=`       | ❌     | ✔️    | ❌  | ❌     | 
|StartsWith         |`_=`        | ❌     | ✔️    | ❌  | ❌     |
|NotStartsWith      |`!_=`       | ❌     | ✔️    | ❌  | ❌     |
|EndsWith           |`\|=`       | ❌     | ✔️    | ❌  | ❌     |
|NotEndsWith        |`!\|=`      | ❌     | ✔️    | ❌  | ❌     |

##### Example
api/students/

?Filter=Midterm>10;and 

&Order=Midterm;desc

&PageNumber=1

&PageSize=2`

The above expression returns us students whose midterms is more than 10, then order by Midterm descending with page number is 1 and page size is 2.

It works! 🚀
For more about query language click here!

### Examples
<img src="https://user-images.githubusercontent.com/3358953/64111010-48ae2a80-cd8c-11e9-8398-c6e811bec5e7.png" width="800" height="450"/>

It works! Perfect!

<img src="https://user-images.githubusercontent.com/3358953/64111026-519efc00-cd8c-11e9-9d5d-4b86485afd6f.png" width="800" height="450"/>

Works for multiple filter logic as well! 🎉

I don't want to make readme page so crowdy. please visit the [wiki page](https://github.com/arslanaybars/Fop/wiki) to see more feature of the package

### Next Things..
- LOGO
- Better unit tests
- Improved sample

### License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details

### Contributing

Everyone is welcome to contribute!

### Acknowledgments

* [DynamicExpresso](https://github.com/davideicardi/DynamicExpresso/ "DynamicExpresso") It helps me to save my hair pulled down! 🙏 Thanks for the great library. It helps me a lot on Filtering.
