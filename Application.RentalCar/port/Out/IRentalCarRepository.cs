//using Domain.Lab120240821;
using Domain.RentalCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RentalCar.port.Out
{
    /// <summary>
    /// ������Ʀs������ - ���μh��X��
    /// </summary>
    /// <remarks>
    /// �������w�q�F�����t�Τ�������ƪ��� CRUD �ާ@�A
    /// �@�����μh����X��A�Ѱ�¦�]�I�h��@���骺��Ʀs���޿�C
    /// ��`�����[�c�]Hexagonal Architecture�^���]�p��h�A
    /// �z�L�̿�����h��{���μh�P��¦�]�I�h���ѽ��C
    /// </remarks>
    public interface IRentalCarRepository
    {
        /// <summary>
        /// �s�W������t�Τ�
        /// </summary>
        /// <param name="car">�n�s�W����������A������@ IVehicle ����</param>
        /// <returns>
        /// �^�� <c>true</c> ��ܦ��\�s�W���������x�s��F
        /// �^�� <c>false</c> ��ܷs�W���ѡA�i���]�]�A�����w�s�b�B������ҥ��ѩθ�Ʈw�s�u���D��
        /// </returns>
        /// <exception cref="ArgumentNullException">�� <paramref name="car"/> �� null �ɩߥX</exception>
        /// <example>
        /// <code>
        /// var newCar = new Car { Model = "Toyota Camry", CC = 2000 };
        /// bool result = repository.AddCar(newCar);
        /// if (result)
        /// {
        ///     Console.WriteLine("�����s�W���\");
        /// }
        /// </code>
        /// </example>
        bool AddCar(IVehicle car);

        /// <summary>
        /// �q�t�Τ��������w������
        /// </summary>
        /// <param name="car">�n��������������A������@ IVehicle ����</param>
        /// <returns>
        /// �^�� <c>true</c> ��ܦ��\�q����x�s�鲾�������F
        /// �^�� <c>false</c> ��ܲ������ѡA�i���]�]�A�������s�b�B�������b�ϥΤ��θ�Ʈw�s�u���D��
        /// </returns>
        /// <exception cref="ArgumentNullException">�� <paramref name="car"/> �� null �ɩߥX</exception>
        /// <remarks>
        /// ���������e���T�{�Ө����ثe�S���Q���ΡA
        /// ��ĳ�b��@�ɥ[�J�������~�ȳW�h�ˬd�C
        /// </remarks>
        /// <example>
        /// <code>
        /// var carToRemove = existingCar;
        /// bool result = repository.RemoveCar(carToRemove);
        /// if (result)
        /// {
        ///     Console.WriteLine("�����������\");
        /// }
        /// </code>
        /// </example>
        bool RemoveCar(IVehicle car);

        /// <summary>
        /// ��s�t�Τ��{����������T
        /// </summary>
        /// <param name="car">�]�t��s��T����������A������@ IVehicle ����</param>
        /// <returns>
        /// �^�� <c>true</c> ��ܦ��\��s������T�����x�s��F
        /// �^�� <c>false</c> ��ܧ�s���ѡA�i���]�]�A�������s�b�B������ҥ��ѩθ�Ʈw�s�u���D��
        /// </returns>
        /// <exception cref="ArgumentNullException">�� <paramref name="car"/> �� null �ɩߥX</exception>
        /// <remarks>
        /// ��s�ާ@�q�`��󨮽����ߤ@�ѧO�X�]�p ID�^�өw��n��s�������A
        /// ��@�����T�O�����ѧO�X���@�P�ʩM��Ƨ���ʡC
        /// </remarks>
        /// <example>
        /// <code>
        /// var updatedCar = existingCar;
        /// updatedCar.Model = "Updated Model Name";
        /// bool result = repository.UpdateCar(updatedCar);
        /// if (result)
        /// {
        ///     Console.WriteLine("������T��s���\");
        /// }
        /// </code>
        /// </example>
        bool UpdateCar(IVehicle car);
    }
}

