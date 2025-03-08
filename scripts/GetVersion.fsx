#r "nuget: FSharp.Data, 6.4.1"
open FSharp.Data

let versionSuffix = match fsi.CommandLineArgs.Length with
                    | n when n > 1 -> "-" + fsi.CommandLineArgs.[1]
                    | _ -> ""

type publishingProperties = XmlProvider<"../properties/GirCore.Publishing.props">

let versionPrefix = publishingProperties.GetSample().PropertyGroups
                    |> Seq.tryFind(fun p -> p.VersionPrefix.IsSome)     
                    |> function
                        |Some vp -> vp.VersionPrefix.Value
                        |None -> "Versionprefix not found"

System.Console.WriteLine(versionPrefix+versionSuffix)
            