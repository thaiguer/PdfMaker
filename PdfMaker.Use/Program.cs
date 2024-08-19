using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;
using PdfMaker.Lib;
using PdfMaker.Lib.DocumentModel;
using PdfMaker.Lib.EntitiesHandle;
using PdfSharp.Quality;
using PdfMaker.Lib.CadModel;

Console.WriteLine("Hello, World!");

string testCadFileFullName = @$"{AppDomain.CurrentDomain.BaseDirectory}\thecadfile.dwg";
string pdfOutputDirectoryPath = @"C:\dev";

var documentInfo = new PdfDocumentInfo(
    title: "thepdffile",
    subject: "Just Some PDF",
    author: Environment.UserName,
    directoryPath: pdfOutputDirectoryPath);

var pdfFromModelSpace = new PdfFromEntities(documentInfo);
var reader = new Reader();
pdfFromModelSpace.CadEntities = reader.ReadFromModelSpace(testCadFileFullName);

//add optional plotstyle //https://github.com/phusband/PiaNO
//or make some way to implement
//index color:thickness and print color
//a json file maybe

pdfFromModelSpace.Create();

PdfFileUtility.ShowDocument(pdfFromModelSpace.PdfDocumentInfo.FileFullName);