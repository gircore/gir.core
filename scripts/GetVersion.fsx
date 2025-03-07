#r "nuget: FSharp.Data, 6.4.1"

open FSharp.Data

type publishingProperties = XmlProvider<"../properties/GirCore.Publishing.props">

let versionPrefix = publishingProperties.GetSample().PropertyGroups
                    |> Seq.tryFind(fun p -> p.VersionPrefix.IsSome)     
                    |> function
                        |Some vp -> vp.VersionPrefix.Value
                        |None -> "Versionprefix not found"

System.Console.WriteLine(versionPrefix)
            