using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeScript
{
    class RegexExamples
    {
    }

    //@"\d+\s"; - number then space

    // \d+ -numbers
    // \s - space

    // ^ - start

    // \b - Start at a word boundary. 
    // \bWORLD\b - not ignore upper or lower case

    // \[(.*?)\] -text in brackets [text]
    // \, - ,
    // \& - &

    // \d+\s\bas\b\s\[(.*?)\]  
    // 24 AS [test],
    // 24 AS [test3232],
}
