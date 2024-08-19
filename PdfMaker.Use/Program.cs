using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;
using PdfMaker.Lib;
using PdfMaker.Lib.DocumentModel;
using PdfMaker.Lib.EntitiesHandle;
using PdfSharp.Quality;

Console.WriteLine("Hello, World!");

var documentInfo = new PdfDocumentInfo(
    title: "SamplePdf",
    subject: "Just Some PDF",
    author: Environment.UserName,
    directoryPath: @"C:\dev");

var pdfFromModelSpace = new PdfFromEntities(documentInfo);
//add entities

//add optional plotstyle //https://github.com/phusband/PiaNO
//or make some way to implement
//index color:thickness and print color
//a json file maybe

pdfFromModelSpace.Create();

PdfFileUtility.ShowDocument(pdfFromModelSpace.PdfDocumentInfo.FileFullName);