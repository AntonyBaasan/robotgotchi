import { UtilService } from './util-service';
import { GlobalSettings } from '../models/index';
import { IMessageBroker, IMessageListener } from '../models/index';

export class DiscoveryService extends IMessageListener {

  constructor(private utilService: UtilService) {
    super();
  }

  listenMessage(broker: IMessageBroker): void {
    broker.registerListeners('globalsettings', async () => {
      const settings = this.getGlobalSettings();
      return { messageType: 'globalsettings', payload: settings };
    });
  }

  getGlobalSettings(): GlobalSettings {
    return {
      webapiurl: this.utilService.getWebApiUrl(),
    } as GlobalSettings;

  }

}
