import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react';
import { Button, Header, Icon, Input, Label, Table } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';

interface Amounts {
    [key: string]: string;
  }


export default observer (function UserForm() {
    const {userStore } = useStore();
    const {users} = userStore;
    const [amounts, setAmounts] = useState<Amounts>({});

    useEffect(() =>{
        userStore.getallusers()
    }, [userStore])

    const handleSaveAmount = async (username: string) => {
        if (userStore) {
            const parsedAmount = parseInt(amounts[username]) || 0;
            console.log('Parsed amount: ', parsedAmount)
            await userStore.setuseramount(parsedAmount, username);
            // Reload data after saving the amount
            userStore.getallusers();
        }
      };

      const handleAmountChange = (username: string, value: string) => {
        setAmounts({ ...amounts, [username]: value });
      };

  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Username</Table.HeaderCell>
          <Table.HeaderCell>Amount</Table.HeaderCell>
          <Table.HeaderCell>Update user total amount, The amount mentioned will be added to existing total</Table.HeaderCell>
          <Table.HeaderCell></Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {users.map(user => (
          <Table.Row key={user.username}>
            <Table.Cell>
                <Header.Content>
                    {user.displayName}
                    <Header.Subheader>{user.username}</Header.Subheader>
                </Header.Content>
            </Table.Cell>
            <Table.Cell >{user.amount}</Table.Cell>
            <Table.Cell>
                <Input labelPosition='right' type='text' placeholder='Amount'>
                    <Label basic>₹</Label>
                    <input 
                        value={amounts[user.username] || ''}
                        onChange={(e) => handleAmountChange(user.username, e.target.value)}                      
                    />
                    <Label>.00</Label>
                </Input>
                </Table.Cell>
                <Table.Cell>
                <Button animated color='teal' onClick={() => handleSaveAmount(user.username)}>
                    <Button.Content visible >Save</Button.Content>
                    <Button.Content hidden>
                        <Icon name='save' />
                    </Button.Content>
                </Button>
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
  );
})