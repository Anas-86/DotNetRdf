using System;
using System.IO;
using System.Net;
using VDS.RDF;
using VDS.RDF.Writing;
using VDS.RDF.Parsing;

namespace TriplesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraph g = new Graph();
            IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
            ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
            ILiteralNode bonjourMonde = g.CreateLiteralNode("Bonjour tout le Monde", "fr");

            g.Assert(new Triple(dotNetRDF, says, helloWorld));
            g.Assert(new Triple(dotNetRDF, says, bonjourMonde));

            // the output file is called HelloWorld.net and is located in 
            // TriplesProject\bin\Debug\net5.0
            NTriplesWriter ntwriter = new NTriplesWriter();
            ntwriter.Save(g, "HelloWorld.nt");

            // for saving the file in another format use the following
            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();
            rdfxmlwriter.Save(g, "HelloWorld.rdf");

            foreach (Triple t in g.Triples)
            {
                Console.WriteLine(t.ToString());
            }

            TurtleParser ttlparser = new TurtleParser();

            //Load using a Filename
            ttlparser.Load(g, "HelloWorld.rdf");

            //Load using a StreamReader
            ttlparser.Load(g, new StreamReader("Example_withStream.ttl"));
        }
        
        // this method will try to select the correct Parser based on the file extension of
        // the file if it corresponds to a standard file extension

        public IGraph ReadGraph(string fileName)
        {
            IGraph g = new Graph();
            FileLoader.Load(g, "somefile.rdf");
            return g;
        }
        
        public IGraph ReadGraphWithouStream_ttl(string fileName)
        {
            IGraph g = new Graph();
            TurtleParser ttlparser = new TurtleParser();
            try
            {
                ttlparser.Load(g, fileName);
            }
            catch (RdfParseException parseEx)
            {
                //This indicates a parser error e.g unexpected character, premature end of input, invalid syntax etc.
                Console.WriteLine("Parser Error");
                Console.WriteLine(parseEx.Message);
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }

            //Load using a Filename
            return g;
        }

        public IGraph ReadGraphWithStream_ttl(string fileName)
        {
            IGraph g = new Graph();
            TurtleParser ttlparser = new TurtleParser();
            try
            {
                ttlparser.Load(g, new StreamReader(fileName));
            }
            catch (RdfParseException parseEx)
            {
                //This indicates a parser error e.g unexpected character, premature end of input, invalid syntax etc.
                Console.WriteLine("Parser Error");
                Console.WriteLine(parseEx.Message);
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }

            //Load using a Filename
            return g;
        }

        public IGraph ReadGraphFromUri(string uri)
        {
            IGraph g = new Graph();
            try
            {
                UriLoader.Load(g, new Uri(uri));
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }
            catch (WebException webException)
            {
                //This represents a web error in reading the RDF file
                Console.WriteLine("Web Error");
                Console.WriteLine(webException.Message);
            }
            // example:
            // UriLoader.Load(g, new Uri("http://dbpedia.org/resource/Barack_Obama"));
            return g;
        }

        
        // Writing graphs.
        
        // Writing graph to a file.
        public void WriteGraphToFile(IGraph g)
        {
            //Assume that the Graph to be saved has already been loaded into a variable g
            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();

            //Save to a File
            rdfxmlwriter.Save(g, "ResultedGraph.rdf");
        }
        
        // Writing graph to a string
        public string WriteGraphToString(IGraph g)
        {
            //Assume that the Graph to be saved has already been loaded into a variable g
            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();

            String data = VDS.RDF.Writing.StringWriter.Write(g, rdfxmlwriter);
            return data;
        }
        
        
        // wokring with graphs

        public void GetAllUirNodes(IGraph g)
        {
            //Assuming we have some Graph g find all the URI Nodes
            foreach (IUriNode u in g.Nodes.UriNodes())
            {
                //Write the URI to the Console
                Console.WriteLine(u.Uri.ToString());
            }
        }

        public bool IsGraphEmpty(IGraph g)
        {
            return g.IsEmpty;
        }

        public BaseTripleCollection GetTriples(IGraph g)
        {
            return g.Triples;
        }
        
        
    }
}