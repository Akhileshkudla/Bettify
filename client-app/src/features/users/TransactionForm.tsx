import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { Header, Table } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';


export default observer (function TranscationForm() {
    const {transactionStore} = useStore();
    const {transcation : {messages}} = transactionStore;

    useEffect(() =>{
        transactionStore.getTransaction();
    }, [transactionStore])

  const messageList = Array.isArray(messages) ? messages : [messages];

  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Transactions</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {messageList.filter(activity => activity).map((activity, index) => (
          <Table.Row key={index}>
            <Table.Cell>
              <Header.Content>
                {activity}
              </Header.Content>
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
  );
})