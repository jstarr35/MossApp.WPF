﻿using MossApp.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MossApp.Core
{
    public class LanguageSetEvent : PubSubEvent<Language>
    {
    }
}
