<h1 align="center">CoubSharper</h1>

<div align="center">

Coub Search API, https://coub.com

[![GitHub license](https://img.shields.io/github/license/ewgraf/CoubSharper.svg)](https://raw.githubusercontent.com/rails/rails/master/MIT-LICENSE)
[![NuGet](https://img.shields.io/badge/nuget-v2.0-orange.svg)](https://www.nuget.org/packages/CoubSharper)
[![.Net](https://img.shields.io/badge/.net%20standard-2.0%2B-blue.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

</div>

# Install
\> `dotnet add package CoubSharper` or `Install-Package CoubSharper` or via `Manage NuGet Packages...`

NuGet: https://www.nuget.org/packages/CoubSharper

# Usage
```csharp
using CoubSharper;
...
using (var client = new CoubClient()) {
	CoubsSearchResponse search = client.SearchCoubs("query");
	// do the staff
}
```

# Sample
```csharp
using System;
using System.Linq;
using CoubSharper;
using Newtonsoft.Json;

namespace CoubSharperTest {
    public class Program {
        public static void Main(string[] args) {			
            using (var client = new CoubClient()) {
                CoubsSearchResponse search = client.SearchCoubs("cosplay", OrderBy.views_count, page: 1);

                var coubs = search.coubs.Select(c => new {
                    Permalink = $"https://coub.com/view/{c.permalink}",
                    Tags = c.tags.Select(t => t.title).ToArray(),
                    Title = c.title,
                    OriginalTitle = c.media_blocks?.external_video?.title
                }).ToArray();

                string coubsJson = JsonConvert.SerializeObject(coubs, Formatting.Indented);
                Console.WriteLine(coubsJson);
            }
        }
    }
}
```
