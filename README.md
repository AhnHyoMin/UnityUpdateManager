# UnityUpdateManager
UpdateManager Performance Test

개인 작업하다, 테스트 한것이라 코드는 깔끔하지 않습니다.


해당 프로젝트의 목적은 UpdateManager의 구현으로 성능상 이득을 가져올 수 있는지에 대한 테스트 

[해당 포스트 읽어볼 것]
https://blogs.unity3d.com/2015/12/23/1k-update-calls/


1000개의 loop 테스트에서는 Update와 UpdateManager의 성능 차이는 크게 없음

5000개의 loop 테스트 부터는  UpdateManager가 성능상 더 빠름.


결론, 실제로 성능상 이득을 가져오긴 하지만 써먹을만한 것 인지는 의문...
