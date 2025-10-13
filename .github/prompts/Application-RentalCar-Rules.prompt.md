# �����t�����μh Application.RentalCar �{���X����

## ���z
Application.RentalCar �O�����t�Ϊ����μh�A�t�d��ջ��A�ȩM��¦�]�I�h�A��{���骺�~�ȥΨҡC���h�ĥ� Hexagonal Architecture (�����[�c) ���]�p�Ҧ��A�z�L Port �M Adapter �өw�q�����M�~���������C

## �[�c�զ�

### 1. Port (��) �h
�w�q���μh����J��X�����A��{�̿�����h�C

#### In Port (��J��)
- **IRentalCarUseCase.cs** - �w�q�����A�Ȫ��D�n�ΨҤ���

#### Out Port (��X��)  
- **IRentalCarRepository.cs** - �w�q��Ʀs��������

### 2. ���ΪA�ȼh
- **RentalCarSerAppServices.cs** - �����A�Ȫ����ε{���A�ȹ�@

---

## �Բ����O����

### IRentalCarUseCase ����
**�ɮצ�m**: `Application.RentalCar\port\In\IRentalCarUseCase.cs`

**�γ~**: �w�q�����t�Ϊ��֤ߥΨҤ����A�@�����μh����J��A�ѤW�h(�p Web API ���)�I�s�C

#### Methods ����:

1. **ToRentCar(IVehicle car)**
   - **�γ~**: �i�樮�����Χ@�~
   - **�Ѽ�**: 
     - `car` (IVehicle): �n���Ϊ���������
   - **�^�ǭ�**: bool - �O�_���\����
   - **�\��**: �B�z�������Ϊ��֤��޿�

2. **GetAllCars()**
   - **�γ~**: ���o�Ҧ��i���Ψ���
   - **�Ѽ�**: �L
   - **�^�ǭ�**: IEnumerable<IVehicle> - �Ҧ����������X
   - **�\��**: �d�ߨæ^�Ǩt�Τ��Ҧ��i�Ϊ������M��

---

### IRentalCarRepository ����
**�ɮצ�m**: `Application.RentalCar\port\Out\IRentalCarRepository.cs`

**�γ~**: �w�q������Ʀs���������A�@�����μh����X��A�Ѱ�¦�]�I�h��@���骺��Ʀs���޿�C

#### Methods ����:

1. **AddCar(IVehicle car)**
   - **�γ~**: �s�W������t�Τ�
   - **�Ѽ�**: 
     - `car` (IVehicle): �n�s�W����������
   - **�^�ǭ�**: bool - �O�_���\�s�W
   - **�\��**: �N������ƫ��[�ƨ����x�s��

2. **RemoveCar(IVehicle car)**
   - **�γ~**: �q�t�Τ���������
   - **�Ѽ�**: 
     - `car` (IVehicle): �n��������������
   - **�^�ǭ�**: bool - �O�_���\����
   - **�\��**: �q����x�s�餤�R�����w����

3. **UpdateCar(IVehicle car)**
   - **�γ~**: ��s������T
   - **�Ѽ�**: 
     - `car` (IVehicle): �n��s����������
   - **�^�ǭ�**: bool - �O�_���\��s
   - **�\��**: �ק����x�s�餤��������T

---

### RentalCarSerAppServices ���O
**�ɮצ�m**: `Application.RentalCar\RentalCarSerAppServices.cs`

**�γ~**: �����A�Ȫ����ε{���A�ȹ�@�A�t�d��եΨҩM��Ʀs���A��{���骺�~�Ȭy�{�C

#### �غc�禡:
**RentalCarSerAppServices(IRentalCarRepository rentalCarRepository, IRentalCarUseCase rentalCarUseCase)**
- **�γ~**: ��l�ƪA�ȡA�`�J�̩ۨ�
- **�Ѽ�**: 
  - `rentalCarRepository` (IRentalCarRepository): ������Ʈw�s������
  - `rentalCarUseCase` (IRentalCarUseCase): �����ΨҤ���
- **�\��**: �z�L�̿�`�J�]�w�A�ȩһݪ��̪ۨ���

#### Methods ����:

1. **GetAllCars()**
   - **�γ~**: ���o�Ҧ��i������
   - **�Ѽ�**: �L
   - **�^�ǭ�**: IEnumerable<IVehicle>? - �����M��(�i�� null)
   - **�\��**: �e���� UseCase �����Ө��o�Ҧ�����
   - **��@�Ӹ`**: �����I�s `_rentalCarUseCase.GetAllCars()`

2. **ToRentCar(IVehicle car)**
   - **�γ~**: ���Ψ���
   - **�Ѽ�**: 
     - `car` (IVehicle): ������T
   - **�^�ǭ�**: bool - �O�_���\����
   - **�\��**: �B�z�������Ϊ�����y�{
   - **��@�Ӹ`**: 
     - �إ߷s�� Car ����A�ƻs��J������ CC �M Model �ݩ�
     - �z�L Repository �����N�����[�J�t��
     - **�`�N**: �ثe��@�s�b�޿���D�A�إߤF�s�������ǤJ��l������ Repository

---

## �]�p�Ҧ��M�[�c�S�I

### 1. Hexagonal Architecture (�����[�c)
- **In Port**: �w�q���μh�����~���ШD������
- **Out Port**: �w�q���μh��~���귽���ݨD����
- **���ΪA��**: ��@���骺�~���޿�M�y�{���

### 2. �̿�����h (Dependency Inversion Principle)
- ���μh�������̿�����@�A�ӬO�̿��H����
- �z�L�غc�禡�`�J��{�P�����X

### 3. ���`�I���� (Separation of Concerns)
- **IRentalCarUseCase**: �M�`��~�ȥΨҩw�q
- **IRentalCarRepository**: �M�`���Ʀs����H
- **RentalCarSerAppServices**: �M�`��y�{��թM�~���޿��@

---

## �ﵽ��ĳ

### RentalCarSerAppServices.ToRentCar ��k
�ثe��@���s�b��b���D�G
```csharp
Car mycar = new Car() { CC = car.CC, Model = car.Model };
return _rentalCarRepository.AddCar(car); // ���ӶǤJ mycar �ӫD car
```

��ĳ�ץ����G
```csharp
return _rentalCarRepository.AddCar(mycar);
```
�Ϊ̪����ϥέ�l�����G
```csharp
return _rentalCarRepository.AddCar(car);