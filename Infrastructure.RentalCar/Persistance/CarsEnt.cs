using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.RentalCar.Persistance
{
    /// <summary>
    /// ������ƹ������O - ��¦�]�I�h��ƫ��[�ƹ���
    /// </summary>
    /// <remarks>
    /// �����O�N�����t�Τ�������Ʀb��Ʈw�������鵲�c�A
    /// �@����¦�]�I�h����Ʀs������]DAO�^�A�t�d�P��Ʈw�i�� ORM �M�g�C
    /// ��`�����[�c�]Hexagonal Architecture�^���]�p��h�A
    /// ���������O�M���B�z��ƫ��[�Ƽh�����ݨD�A�P���ҫ������A
    /// �z�L�A�t���Ҧ��P���h�� IVehicle �����i���ഫ�C
    /// 
    /// �����O�ϥ� Entity Framework Core �i���Ʈw�ާ@�A
    /// �䴩 Code First �覡�إ߸�Ʈw���c�C
    /// </remarks>
    public class CarsEnt
    {
        /// <summary>
        /// �����ߤ@�ѧO�X
        /// </summary>
        /// <value>
        /// �������D���ѧO�X�A�Ѹ�Ʈw�۰ʲ��͡C
        /// ���Ȧb��Ʈw�������O�ߤ@���A�Ω��ѧO�S�w�������O���C
        /// </value>
        /// <remarks>
        /// �b Entity Framework Core ���A���ݩʳq�`�|�Q�]�w���۰ʻ��W���D��C
        /// ��s�W�������Ʈw�ɡA��Ʈw�|�۰ʤ��t�@�Ӱߤ@�� ID �ȡC
        /// </remarks>
        /// <example>
        /// <code>
        /// var car = new CarsEnt { Model = "Toyota Camry" };
        /// // �s�W���Ʈw��AId �|�Q�۰ʳ]�w
        /// context.Cars.Add(car);
        /// context.SaveChanges();
        /// Console.WriteLine($"�s���� ID: {car.Id}");
        /// </code>
        /// </example>
        public int Id { get; set; }

        /// <summary>
        /// ���������W��
        /// </summary>
        /// <value>
        /// �����������Ϋ~�P�W�١A�Ҧp�G"Toyota Camry"�B"Honda Civic" ���C
        /// ����줣�ର�ŭȡA�O�ѧO���������n��T�C
        /// </value>
        /// <remarks>
        /// ���ݩʹ�������ҫ���������������T�A
        /// �b�i���쪫��P��ƹ����ഫ�ɡA�ݭn���T�M�g�����C
        /// ��ĳ�b��Ʈw�h���]�w�A���r����׭���M�D�Ŭ����C
        /// </remarks>
        /// <example>
        /// <code>
        /// var car = new CarsEnt 
        /// { 
        ///     Model = "BMW 320i",
        ///     cc = 2000,
        ///     Type = "Sedan"
        /// };
        /// </code>
        /// </example>
        public string Model { get; set; }

        /// <summary>
        /// �����Ʈ�q�]�ߤ褽���^
        /// </summary>
        /// <value>
        /// �����������Ʈ�q�A�H�ߤ褽���]cc�^�����C
        /// ���ȥ���������ơA�Ω�p�⯲���M�����������šC
        /// </value>
        /// <remarks>
        /// �Ʈ�q�O���������n�W�椧�@�A�v�T�����p��M���������C
        /// �b�����t�Τ��A�q�`�|�ھڱƮ�q�ӰϤ����������ũM�w�������C
        /// ��ĳ�b���μh�θ�Ʈw�h���[�J�A��������ҡA�T�O���Ȭ��X�z�d�򤺪�����ơC
        /// </remarks>
        /// <example>
        /// <code>
        /// // �@��p�����Ʈ�q�d��
        /// var smallCar = new CarsEnt { cc = 1600 };
        /// 
        /// // �������Ʈ�q�d��  
        /// var mediumCar = new CarsEnt { cc = 2000 };
        /// 
        /// // �j�����Ʈ�q�d��
        /// var largeCar = new CarsEnt { cc = 3000 };
        /// </code>
        /// </example>
        public int cc { get; set; }

        /// <summary>
        /// �����s�y���
        /// </summary>
        /// <value>
        /// �������X�t�s�y����A�Ω�P�_�����~���M���⨮�����ȡC
        /// ������|�v�T�����p��M�������i���Ϊ��A�C
        /// </value>
        /// <remarks>
        /// �s�y����O���������n�ݩʡA�b�����~�Ȥ��Ω�G
        /// 1. �p�⨮�����©M�����վ�
        /// 2. �P�_�����O�_�ŦX���μзǡ]�Ҧp�G���W�L�S�w�~���^
        /// 3. ���ѵ��Ȥ�ѦҪ�������T
        /// 
        /// �b����x�s�ɫ�ĳ�ϥ� UTC �ɶ��榡�A�קK�ɰ��ഫ���D�C
        /// </remarks>
        /// <example>
        /// <code>
        /// var newCar = new CarsEnt 
        /// { 
        ///     Model = "Toyota Prius",
        ///     ManufacturingDate = new DateTime(2023, 1, 15),
        ///     cc = 1800
        /// };
        /// 
        /// // �p�⨮��
        /// var carAge = DateTime.Now.Year - newCar.ManufacturingDate.Year;
        /// Console.WriteLine($"����: {carAge} �~");
        /// </code>
        /// </example>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        /// <value>
        /// ���������������A�Ҧp�G"�⨮"�B"��Ȩ�"�B"�]��"�B"�f��" ���C
        /// �����Ω󨮽������M�z��\��C
        /// </value>
        /// <remarks>
        /// ���������O�����t�Τ����n�������̾ڡA�Ω�G
        /// 1. �Ȥ�ھڻݨD�z��A�X����������
        /// 2. �t�ήھ������M�Τ��P�������p���޿�
        /// 3. �����޲z�M�w�s����
        /// 
        /// ��ĳ�b���μh�w�q�зǪ����������T�|�A�T�O��Ƥ@�P�ʡC
        /// �b������Τ��A�i�H�Ҽ{�N�����P���ҫ����� VehicleType �T�|�i��M�g�C
        /// </remarks>
        /// <example>
        /// <code>
        /// // ���P�����������d��
        /// var sedan = new CarsEnt { Type = "�⨮", Model = "Toyota Camry" };
        /// var suv = new CarsEnt { Type = "��Ȩ�", Model = "Honda CR-V" };
        /// var sports = new CarsEnt { Type = "�]��", Model = "BMW Z4" };
        /// 
        /// // �ھ������z�郞��
        /// var sedanCars = context.Cars.Where(c => c.Type == "�⨮").ToList();
        /// </code>
        /// </example>
        public string Type { get; set; }
    }
}
