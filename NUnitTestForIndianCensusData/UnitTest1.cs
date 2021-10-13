using IndiaCensusDataDemo;
using IndiaCensusDataDemo.DataDAO;
using IndiaCensusDataDemo.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTestForIndianCensusData
{
    [TestFixture]
    public class UnitTest1
    {
        string stateCodePath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\IndiaStateCode.csv";
        string stateCensusPath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\IndiaStateCensusData.csv";
        string wrongCensusPath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\IndiaStateData.csv";
        string wrongStateCodePath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\IndiaCode.csv";
        string wrongTypeStateCodePath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\IndiaStateCode.txt";
        string wrongHeaderStateCodePath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\WrongIndiaStateCode.csv";
        string wrongHeaderStateCensusPath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\WrongIndiaStateCensusData.csv";
        string delimiterStateCodePath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\DelimiterIndiaStateCode.csv";
        string delimiterStateCensusPath = @"C:\Users\Hanif\source\repos\IndianStateCensus\NewIndianCensus058\IndianCensusCSVData015\CSVFiles\DelimiterIndiaStateCensusData.csv";
        IndiaCensusDataDemo.CSVAdapterFactory csv = null;
        //Dictionary<string, CensusDTO> totalRecords;
        Dictionary<string, CensusDataDAO> stateRecords,totalRecords;

        [SetUp]
        public void SetUp()
        {
            csv = new IndiaCensusDataDemo.CSVAdapterFactory();
           // totalRecords = new Dictionary<string, CensusDTO>();
            stateRecords = new Dictionary<string, CensusDataDAO>();
        }
        /// TC 1.1
        /// Giving the correct path it should return the total count from the census
        [Test]
        public void GivenStateCensusCSVShouldReturnRecords()
        {
            stateRecords = csv.LoadCsvData(IndiaCensusDataDemo.CensusAnalyser.Country.INDIA, stateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(29, stateRecords.Count);
        }
        /// TC 1.2
        /// Giving incorrect path should return file not found custom exception
        [Test]
        public void GivenIncorrectFileShouldThrowCustomException()
        {
            try
            {
                var customException = Assert.Throws<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                //total no of rows excluding header are 29 in indian state census data.
                //Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, customException.exception);
            }
            catch(CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.FILE_NOT_FOUND);
        }
        /// TC 1.3
        /// Giving wrong type of path should return invalid file type custom exception
        [Test]
        public void GivenWrongTypeReturnsCustomException()
        {
            try
            {
                var customException = Assert.Throws<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeStateCodePath, "SrNo,State Name,TIN,StateCode"));
                Assert.AreEqual(customException.exception, CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// TC 1.4
        /// Giving wrong delimiter should return incorrect delimiter custom exception
        [Test]
        public void GivenWrongDelimeterReturnsCustomException()
        {
            try
            {
                var censusException = Assert.Throws<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, delimiterStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                Assert.AreEqual(censusException.exception, CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// TC 1.5
        /// Giving wrong header type should return incorrect header type custom exception
        [Test]
        public void GivenWrongHeaderReturnsCustomException()
        {
            try
            {
                var censusException = Assert.Throws<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderStateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm"));
                Assert.AreEqual(censusException.exception, CensusAnalyserException.ExceptionType.INCORRECT_HEADER);
            }
            catch (CensusAnalyserException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
