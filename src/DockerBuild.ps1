$appVersion = "v2"

cd C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsProxy
docker build -f "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsProxy\Dockerfile" -t austindimmer/paths-proxy:$appVersion --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=PathsProxy" "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsProxy"
cd C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppOne
docker build -f "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppOne\Dockerfile"  --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=PathsUpstreamAppOne" "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppOne"
cd C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppTwo
docker build -f "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppTwo\Dockerfile" -t austindimmer/paths-upstream-app-two:$appVersion --target base --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=PathsUpstreamAppTwo" "C:\Dev\RecruitmentTests\Adobe\ProxyKit\src\PathsUpstreamAppTwo"

# docker ps -a
# docker container ls
# docker image ls



