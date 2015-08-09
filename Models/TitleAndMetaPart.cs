﻿using Orchard.ContentManagement;

namespace Berry.SEOKit.Models {
    public class TitleAndMetaPart : ContentPart {
        public string Title {
            get { return this.Retrieve(p => p.Title); }
            set { this.Store(p => p.Title, value); }
        }

        public string Description {
            get { return this.Retrieve(p => p.Description); }
            set { this.Store(p => p.Description, value); }
        }

        public string Keywords {
            get { return this.Retrieve(p => p.Keywords); }
            set { this.Store(p => p.Keywords, value); }
        }
    }
}