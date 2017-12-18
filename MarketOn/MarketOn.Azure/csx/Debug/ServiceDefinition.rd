<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MarketOn.Azure" generation="1" functional="0" release="0" Id="727dbc4a-9074-4809-b7e1-faa573edf000" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MarketOn.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MarketOn:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/LB:MarketOn:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MarketOn:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MapMarketOn:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MarketOnInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MapMarketOnInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MarketOn:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOn/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMarketOn:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOn/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMarketOnInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOnInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MarketOn" generation="1" functional="0" release="0" software="C:\Users\migue\Desktop\MarketOn\MarketOn\MarketOn.Azure\csx\Debug\roles\MarketOn" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MarketOn&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MarketOn&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOnInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOnUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOnFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MarketOnUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MarketOnFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MarketOnInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="fbda14c7-5ecc-484d-8559-ff9f55da3329" ref="Microsoft.RedDog.Contract\ServiceContract\MarketOn.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="22282d0d-ed04-4e11-9be6-5478076081f1" ref="Microsoft.RedDog.Contract\Interface\MarketOn:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MarketOn.Azure/MarketOn.AzureGroup/MarketOn:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>