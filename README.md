# CoubSharper
Coub Search API of https://coub.com

[![GitHub license](https://img.shields.io/github/license/:user/:repo.svg)](https://raw.githubusercontent.com/ewgraf/CoubSharper/master/LICENSE)
[![NuGet](https://img.shields.io/badge/nuget-v1.0-orange.svg)](https://www.nuget.org/packages/CoubSharper)

# Install
PM> `Install-Package CoubSharper` from nuget https://www.nuget.org/packages/CoubSharper

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
                CoubsSearchResponse search = client.SearchCoubs(query: "cosplay", OrderBy.views_count, page: 1);

                var coubs = search.coubs.Select(c => new {
                    Permalink = $"https://coub.com/view/{c.permalink}",
                    Tags = c.tags.Select(t => t.title).ToArray(),
                    Title = c.title,
                    OriginalTitle = c.media_blocks.external_video.title
                }).ToArray();

                string coubsJson = JsonConvert.SerializeObject(coubs, Formatting.Indented);
                Console.WriteLine(coubsJson);
            }
        }
    }
}
```
