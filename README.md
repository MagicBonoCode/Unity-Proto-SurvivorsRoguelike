# Unity-Proto-SurvivorsRoguelike

![Animation](https://github.com/MagicBonoCode/Unity-Proto-SurvivorsRoguelike/assets/165194787/4255ff5f-2420-4ab1-bdd6-6c5ec095aa3b)

**프로젝트 정보**

엔진 : Unity

엔진 버전 : 2022.3.21f1

**사용 Unity 패키지**

- Addressable
- Cinemachine

**사용 Asset**

- [언데드 서바이버 에셋 팩](https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068)
- [Cartoon FX Remaster Free](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565)

## **설명**

### **매니저 시스템**

해당 프로젝트의 매니저 시스템은 https://github.com/MagicBonoCode/Unity-Practice-ManagerSystem 기반으로 개발되었습니다.

### **게임 오브젝트 구조**

게임에서 제어되는 모든 오브젝트는 동일한 베이스 동작을 공유할 수 있도록 BaseObject를 상속합니다.
특히 BaseObject는 유니티의 라이프사이클 메서드를 랩핑하여 사용함으로써 추상화 수준을 높이고 코드를 보다 명확하게 구성할 수 있습니다. 이러한 메서드 랩핑을 통해 라이프사이클과 관련된 로직을 한 곳에서 관리할 수 있으므로 코드의 유지보수성이 향상됩니다.
또한 각 라이프사이클 메서드는 다음과 같은 고유한 목적에 맞게 사용됩니다:

1. **Awake()**: Init()으로 랩핑되어 오브젝트의 초기화를 위해 한 번만 호출되도록 합니다.
2. **OnEnable()**: OnEnableObject()으로 랩핑되어 오브젝트가 활성화될 때 필요한 초기값을 재설정합니다.
3. **Start()**: StartObject()로 랩핑되어 첫 프레임이 렌더링되기 전에 호출되는 작업을 수행합니다.
4. **FixedUpdate()**: FinxedUpdateController()로 랩핑되어 주로 오브젝트 간 물리 연산에 사용됩니다.
5. **Update()**: UpdateController()로 랩핑되어 주로 플레이어의 입력 처리, 애니메이션 업데이트 등의 작업이 수행됩니다.
6. **LateUpdate()**: LateUpdateController()로 랩핑되어 주로 오브젝트의 스라이트 작업 및 카메라 a무빙 등의 작업에 사용됩니다.
7. **OnDisable()**: OnDisableObject()로 랩핑되어 주로 오브젝트의 비활성화 시 필요한 작업을 수행합니다.
8. **OnDestroy()**: OnDestoryObject()로 랩핑되어 인스턴스가 파괴될 때 호출되는 메서드로, 메모리 해제나 리소스 정리 등의 작업에 사용됩니다.

또한 BaseObject를 포함 하위 추상 클래스는 “Base” 접두사를 포함합니다.
