# SEOKit for Orchard

SEOKit is an Orchard module containing a collection of features for 
SEO related things. 

The following features are currently supported:

  - Custom page titles
  - Custom meta data
  - Robots.txt configuration
 
### Custom page titles and meta data
Support for custom page titles and meta data can be added to any 
content type by attaching the TitleAndMeta part. A default value 
can be provided for the title and meta description. The default 
value will be used if the value is not explicitly overriden. 
Standard Orchard tokens can be used in default values. 

![Title and Meta Settings](https://raw.githubusercontent.com/joshberry/Berry.SEOKit/master/Content/title-meta-settings.png)

Override checkboxes determine if content editors are able to 
explicitly override the default values when editing content items.

![Title and Meta Settings](https://raw.githubusercontent.com/joshberry/Berry.SEOKit/master/Content/title-meta-part.png)

### Robots.txt configuration
The robots configuration feature provides a new site setting
that accepts the content for a robots.txt file and exposes it
from the root of the site. 

![Robots Settings](https://raw.githubusercontent.com/joshberry/Berry.SEOKit/master/Content/robots-settings.png)